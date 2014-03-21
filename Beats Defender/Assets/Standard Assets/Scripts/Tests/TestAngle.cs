using UnityEngine;
using System.Collections;

public class TestAngle : MonoBehaviour {
	private Transform player;
	private float damping = .5f;		//for smooth follow
	private Vector3 randomPosition= new Vector3(0,0,0);
	private float random;
	private Quaternion rotation;
	
	
	IEnumerator Start () {
		player=GameObject.FindWithTag("Player").transform;
		transform.rotation=Quaternion.identity;
		InvokeRepeating("Randomize",1,3);
		while(true){
			randomPosition=new Vector3(13*Mathf.Cos(random), -27*Mathf.Sin(random) ,13*Mathf.Sin(random) );
			rotation = Quaternion.LookRotation((player.position+randomPosition) - transform.position);
			yield return new WaitForSeconds(1);
		}
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		// Look at and dampen the rotation
			
			transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * damping);
		
			
	}
	
	void Randomize(){
		
		random=Random.Range(-180,180)*(Mathf.PI/180);
//		Debug.Log(random*(Mathf.PI/180));
	}
}
