using UnityEngine;
using System.Collections;

public class DissolveOnSceneLoad : MonoBehaviour {
	public float dissolveSpeed = 0.1f;
	float sliceAmount;
	bool dissolve = false;

	void Start(){
		dissolve = true;
	}
	
	void Update () {				
		if(dissolve){
			sliceAmount -= Time.deltaTime * dissolveSpeed;
			transform.renderer.material.SetFloat("_DissolvePower", 0.65f + Mathf.Sin(0.9f)*sliceAmount);
     		if(renderer.material.GetFloat("_DissolvePower") < -0.5f)
				dissolve = false;
		}
	}
}
