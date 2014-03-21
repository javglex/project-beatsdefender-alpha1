using UnityEngine;
using System.Collections;

public class MouseLookAt2D : MonoBehaviour {

private Vector3 rotationDirection ;
private Quaternion rotation;

public  JoyStick moveTouchPad2;
public JoyStick  moveTouchPad;

void Update(){

	if (moveTouchPad2.position.x!=0||moveTouchPad2.position.y!=0){
		rotationDirection =  new Vector3(moveTouchPad2.position.x, 0 , moveTouchPad2.position.y);
    	rotationDirection = rotationDirection.normalized;

    	//Create a rotation facing that point.
    	rotation = Quaternion.LookRotation(rotationDirection);
    	transform.localRotation =  Quaternion.Lerp(transform.localRotation,rotation,4.7f*Time.deltaTime);
	}else if (moveTouchPad.position.x!=0||moveTouchPad.position.y!=0){
	
		rotationDirection =  new Vector3(moveTouchPad.position.x, 0 , moveTouchPad.position.y);
    	rotationDirection = rotationDirection.normalized;

    	//Create a rotation facing that point.
    	rotation = Quaternion.LookRotation(rotationDirection);
    	transform.localRotation =  Quaternion.Lerp(transform.localRotation,rotation,4.7f*Time.deltaTime);
	
	
	}
}
}
