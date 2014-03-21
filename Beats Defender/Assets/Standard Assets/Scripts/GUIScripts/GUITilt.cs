using UnityEngine;
using System.Collections;

public class GUITilt : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localEulerAngles=new Vector3(Mathf.LerpAngle (transform.localEulerAngles.x,Input.acceleration.y*25,5*Time.deltaTime),0,Mathf.LerpAngle (transform.localEulerAngles.z,Input.acceleration.x*25,5*Time.deltaTime));

	}
}
