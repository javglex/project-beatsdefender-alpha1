using UnityEngine;
using System.Collections;

public class Text3D : MonoBehaviour {
	
	public GUIText text3d;
	public Transform player;
	private float distance=0;
	public float damping = 6.0f;
	public bool smooth = true;
	//public ObjectLabel objText;
	//public ObjectLabel label;
	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
	
			//distance= Vector3.Distance(player.position, label.target.position);
			//text3d.text=""+(int)(distance/3)+" m";

	//text3d.characterSize=.017f*distance;
	}
	
	/*void LateUpdate () {
	if (player&&distance>5f) {
		if (smooth)
		{
			// Look at and dampen the rotation
			var rotation = Quaternion.LookRotation(player.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
		}
		else
		{
			// Just lookat
		    transform.LookAt(player);
		}
	}
}*/

}
