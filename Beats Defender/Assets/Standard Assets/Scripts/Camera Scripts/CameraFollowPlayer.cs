using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {
	
	public Transform target; 

	public Vector3 relativePosition;

 

	void Start() {

    relativePosition = target.transform.position - transform.position;

	}

 

	void Update () {

    transform.position = target.transform.position - relativePosition;

	}
}
