using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFlockController : MonoBehaviour {
	
	private List<GameObject> enemies = new List<GameObject>(); 		//the list of the enemies
	public GameObject enemy;		//enemy that will be added to list
	private EnemyBehavior enemScript;
	// Use this for initialization
	void Start () {
	
		
		for (int i=0; i<10; i++){
			
			enemies.Add(null);
			enemies[i]=Instantiate(enemy, transform.position, Quaternion.identity) as GameObject;
			enemies[i].transform.parent=this.transform;
			enemScript=enemies[i].GetComponentInChildren<EnemyBehavior>();
			enemScript.SetID(i);
		}

	}
	
	// Update is called once per frame
	//void Update () {
	
	//}
	
	public void DestroyEnemy(int id){
		enemies[id].SetActive(false);			//deactivaes enemy, doesn't destroy it
		InstantiateEnemy(id);
	
	}
	
	
	
	void InstantiateEnemy(int id){
		enemies[id].SetActive(true);			//activates enemy, doesn't create it
		enemies[id].SendMessage("Start");			//send "Start" message to reset the variables that have been called from "previous"

	}
}
