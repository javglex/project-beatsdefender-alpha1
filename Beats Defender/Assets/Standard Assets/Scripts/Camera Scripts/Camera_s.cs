using UnityEngine;
using System.Collections;

public class Camera_s : MonoBehaviour {
	
	public Transform characterMain;		//where main camera will "cling" on to (just set height)
//	public Transform camera;		//actual camera 
	// Use this for initialization
	void Start () {
		transform.position=new Vector3(characterMain.position.x,15 ,characterMain.position.z+5f);
	}
	
	// Update is called once per frame
	float zoom=0;
	void Update () {
		zoom=Mathf.Lerp(zoom,AnalyzeMusic.rmsValue*15,1*Time.deltaTime);
		transform.position=new Vector3(0,20+zoom,2.2f) ;	//same speed as ship automated translation

	}
}
