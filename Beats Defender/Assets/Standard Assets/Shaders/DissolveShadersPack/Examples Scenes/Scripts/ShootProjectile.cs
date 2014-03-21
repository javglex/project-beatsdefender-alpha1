using UnityEngine;
using System.Collections;

public class ShootProjectile : MonoBehaviour {
	public Rigidbody projectile;
		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0)){
			Rigidbody proj = Instantiate(projectile, transform.position, Quaternion.identity) as  Rigidbody;
			proj.AddForce(transform.TransformDirection(Vector3.forward) * 2000.0f);
		}
	
	}
}
