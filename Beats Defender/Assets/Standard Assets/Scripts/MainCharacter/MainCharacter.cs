using UnityEngine;
using System.Collections;

public class MainCharacter : MonoBehaviour {
	
	public Joystick moveTouchPad;
	public Animator shipAnim;
	private Touch touch;

	// Use this for initialization
	void Start () {
		transform.position=new Vector3(0,0,0);	//init position
	}
	
	// Update is called once per frame
	void Update () {
		KeyboardControls();	//controlship with keys
		TouchControls ();

	}
	
	void KeyboardControls(){
		
		Movement ( Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

	}
	
	void TouchControls(){
		
		TouchDragDistance();
		Movement (-distance.x,-distance.y);

		
	}
	void Movement(float datax,float dataz){

		//first vector3 automated translation, second vector3 player made translation
		transform.position+=new Vector3(0,0,.01f*Time.deltaTime) + new Vector3(2*datax*Time.deltaTime,0,2*dataz*Time.deltaTime);

		if (datax<0){
			shipAnim.SetBool("RotateRight",false);//turn off rotate right animation
			shipAnim.SetBool("RotateLeft",true); //turn on rotate left animation
		}else if (datax>0){
			shipAnim.SetBool("RotateLeft",false);	//vice versa
			shipAnim.SetBool("RotateRight",true);
		}else {
			shipAnim.SetBool("RotateLeft",false); //when both are false, idle anim plays
			shipAnim.SetBool("RotateRight",false);
		}

	}
	private Vector2 startPosition;
	private Vector2 endPosition;
	private Vector2 distance;		//distance between start position and end position, only x axis
	private void TouchDragDistance(){
		
		
		if (Input.touchCount==0){
			distance=new Vector2(0,0);
			return;			//if not touch, return to top of this function
		}
		touch = Input.GetTouch(0);
		if(touch.phase == TouchPhase.Began){
			startPosition = touch.position;
		}
		if(touch.phase == TouchPhase.Moved){
			endPosition = touch.position;
			distance.x=(startPosition.x-endPosition.x)/100;
			distance.x=Mathf.Clamp (distance.x,-1,1);
			
			distance.y=(startPosition.y-endPosition.y)/50;
			distance.y=Mathf.Clamp (distance.y,-1,1);
		}
		

		
		Debug.Log ("start: "+ startPosition+"end: "+endPosition+"distance: "+distance);
		//return distance;
		
	}
}
