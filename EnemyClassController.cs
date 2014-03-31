// Javier Gonzalez
//The basic enemy class, blueprint for basic enemies. 
//Each enemy under the basic teir might vary slightly 
//Constructor requires at least a gameobject (a gameobject prefab with a sprite)
using UnityEngine;
using System.Collections;

public class BasicEnemyClass {

	
	static public int nObjectEnemies;		//number of total enemy instances
	
	private int health;
	private int ID;			
	private float speed;	//speed of movement
	int prevTarget;		//previous target id, for releasing a square
	int currTarget;		//current target id, to keep track of current square taken
	private Vector2 posTarget;		//the target to where this object wants to move in world space, used for lerping
	private Vector2 thisPosition;	//vector 2 position of this object, no need for 3 dimensions

	private GameObject enemy;
	
	// Use this for initialization
	public BasicEnemyClass(GameObject sprite, Vector2 pos ){
		health=100;
		ID=nObjectEnemies;
		speed=2;
		currTarget=-1;	//no current target, -1 is wild
		prevTarget=-1;
		thisPosition = new Vector2 (pos.x, pos.y);	//position of where object spawns
		enemy=sprite;
		nObjectEnemies++;
	}
	
	public void PositionTarget(int pTarg){	//aquire ye first target

		for (int j=0; j<EnemyGrid.enGrid.Length/7;j++){		//go into grid filling up first available rows	
			for (int i=j; i<EnemyGrid.enGrid.Length;i+=7){
				if (EnemyGrid.enGrid[i].isSpaceFree()){
					posTarget=EnemyGrid.enGrid [i].getPos();
					EnemyGrid.enGrid [i].occupiedBy (enemy);
					prevTarget=i;
					currTarget=i;		//curr target now valid
					return;
				}
			}
		}	
				//else iterate through any available targets
		for (int i=0; i<EnemyGrid.enGrid.Length; i++){		//if no front rows available, just go into any available
			if (EnemyGrid.enGrid[i].isSpaceFree()){
				posTarget=EnemyGrid.enGrid [i].getPos();
				EnemyGrid.enGrid [i].occupiedBy (enemy);
				prevTarget=i;
				currTarget=i;		
				
				return;
			}
			posTarget=thisPosition;	//else sit there and wait in spawn point until new position opens up (worst case scenario)
		}
		
	}
	
	public void UpdateTargetPosition(){		//in case the grid has moved to a new position
		if(currTarget!=-1)		//if currtarget isn't wild follow ur target
			posTarget=EnemyGrid.enGrid [currTarget].getPos();
	}
	public void MoveTowardsTarget(){			//move towards ye target
		thisPosition=Vector2.Lerp (thisPosition,		//lerping stuff, nice smooth movement
		                                  new Vector2(posTarget.x,posTarget.y),speed*Time.deltaTime);
		enemy.transform.position = new Vector3 (thisPosition.x, 0, thisPosition.y);

	}
	public void ChangeTarget(){			//change to a new target (square space) on the grid
		if (currTarget%7==0)	//if currently in front squares, exit, don't change
			return;	
		for (int j=0; j<EnemyGrid.enGrid.Length/7;j++){		
			for (int i=0; i<EnemyGrid.enGrid.Length;i+=7){
				if (EnemyGrid.enGrid[i].isSpaceFree()){
					EnemyGrid.enGrid[prevTarget].releaseSquare();		//free up the previously taken square
					PositionTarget (i); //go to your new square
					return;
				}
			}
		}	
		
				//iterate through any available targets
		for (int i=0; i<EnemyGrid.enGrid.Length; i++){
			if (EnemyGrid.enGrid[i].isSpaceFree()){
				EnemyGrid.enGrid[prevTarget].releaseSquare();		//free up the previously taken square
				PositionTarget (i); 
				return;
			}
		}
		
	}
	

	public bool Damaged(int dmg){	 //function also doubles as  a check to see whether health ammnt kills it or not
		bool dead=false;
		health-=dmg;
		if (health<=0){
			dead=true;
		}
		return dead;			
		
	}
	public void Destroyed(){		//reset a bunch of bunch
		Debug.Log ("ID: "+ID+"CubeDiedOn: "+currTarget);
		EnemyGrid.enGrid[currTarget].releaseSquare();		//free up the taken square
		currTarget=-1;
		prevTarget=-1;
		health=100;
		enemy.transform.position=thisPosition=new Vector2(0,10);		//reset position
	}


}


public class EnemyClassController:MonoBehaviour {		//this class is the controller for the basicenemyclass, 
//basically it controls the enemy using the built in functions in the basicenemyclass

	//public GameObject basicEnemyType1;
	private Vector2 pos;
	BasicEnemyType1 enType1;

	
	public void ForceStart () {		//since this object is immediately disabled after being created (object pooling) it needs a manual start function
		pos = new Vector2 (0, 10);
		enType1=new BasicEnemyType1(this.gameObject, pos);
		//enType1.Parent(this.transform);		//put all the instanced enemies under this gameobject, for organization
	}
	public void Activate(){		//part of object pooling
		enType1.PositionTarget(0);
		InvokeRepeating ("ChangeTarget", 4, 5);
		InvokeRepeating ("SlowUpdate",.3f,.3f);

	}	
	private void Deactivate(){		//also a part of object pooling
		enType1.Destroyed();		//will reset variables and possesions (such as square space) and stuff
		CancelInvoke ("SlowUpdate");
		CancelInvoke ("ChangeTarget");
		this.gameObject.SetActive(false);
	}
	
	
	void ChangeTarget(){
		
		enType1.ChangeTarget();
	}
	void SlowUpdate(){
		enType1.UpdateTargetPosition();
	}
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag=="Player"){
			Deactivate();
			
		}
	}
	
	void OnParticleCollision(GameObject other){
		if (enType1.Damaged(10))	
			Deactivate();
		
	}

	//Called in the enemy spawn controller, under the update function
	public void MoveTowardsTarget () {
		enType1.MoveTowardsTarget ();
	}
}


