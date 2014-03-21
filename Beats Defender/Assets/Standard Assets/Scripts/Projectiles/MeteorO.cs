using UnityEngine;
using System.Collections;

public class MeteorO : MonoBehaviour {
	
	
	// Update is called once per frame
	void Update () {
		AutoRun ();
	}
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag=="LowerBoundary"){
			Deactivate();
			
		}
		
	}
	
	public void Activate(Vector2 pos){
		transform.position=new Vector3(pos.x,0,pos.y);
	}
	
	private void Deactivate(){
		this.gameObject.SetActive(false);
	}
	
	private void AutoRun(){
		transform.Translate(new Vector3(0,0,-3*Time.deltaTime));
	}
	
}
