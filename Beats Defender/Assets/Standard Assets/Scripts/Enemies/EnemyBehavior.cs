using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	
	
	private Transform player;
	private Vector3 screenPlayer;		//world to screen space vector of player
	private Vector3 screenEnemy;		//same but for enemy
	private float degrees;		//rotation necessary to look at player
	private float degreesLerp;		//smooth look degree rotation
	private float distance=1;		//distane between enemy and player (going to be used to tilt ship to be parallel to sphere curve)
	private float tiltAngle;	//tilt angle so that the ships properly allign with the planet
	private float rotAmmount;		//the ammount of ship tilt rotation, goes with tilt angle
	private float angleToPlanet;		//enemy's angle relative to planet
	private int id;				//identification to send message back to flock controller to "destroy" apropriate ship
	private EnemyFlockController enemyFlockController;		//access script function DestroyEnemyID(int)

	
	
	void Start () {
		enemyFlockController=GameObject.Find("EnemyInstant").GetComponent<EnemyFlockController>();
		player=GameObject.FindWithTag("Player").transform;	
		InvokeRepeating("Check",.4f,.4f);
	}
	
	void Check(){
		screenPlayer=Camera.main.WorldToScreenPoint (player.position);		
		screenEnemy=Camera.main.WorldToScreenPoint(transform.position);
		distance=Vector2.Distance(screenPlayer,screenEnemy);
		degrees=Mathf.Atan((-screenEnemy.y+screenPlayer.y)/(-screenEnemy.x+screenPlayer.x))* Mathf.Rad2Deg;  //fix

	}
	
	void Update(){

		LookAt();

	}
	
	void LookAt(){
		//Debug.Log(degrees);
	
		rotAmmount=(2*Mathf.PI*WorldSystem.planetRadius)/(angleToPlanet*WorldSystem.planetRadius);
		tiltAngle=(90*rotAmmount);	

		if (screenPlayer.x>screenEnemy.x){
			transform.localScale=new Vector3(0.588809f,1,0.4409949f);
		}else transform.localScale=new Vector3(0.588809f,-1,0.4409949f);
		degreesLerp=Mathf.Lerp(degreesLerp,degrees,2*Time.deltaTime);		//smoothly transform the degrees
		
		//degrees=Mathf.Repeat(degrees,180);
		
		transform.eulerAngles= new Vector3(90,0,(degreesLerp+90));
		transform.localEulerAngles=new Vector3(tiltAngle,transform.localEulerAngles.y,transform.localEulerAngles.z);
		
		//transform.localRotation = Quaternion.Euler(new Vector3(0,0,degrees+90));
		//Debug.Log(transform.localRotation);
		
		//transform.eulerAngles= new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,Vector2.Angle(new Vector2(screenEnemy.x,screenEnemy.y),new Vector2(screenPlayer.x,screenEnemy.y))+90);

		//transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 35f);
		
		
	}
	
	
	public void SetID(int identification){
		id=identification;
	}

	IEnumerator OnParticleCollision (){
		enemyFlockController.DestroyEnemy(id);
		yield return new WaitForSeconds(2);
	}
		
		
}
