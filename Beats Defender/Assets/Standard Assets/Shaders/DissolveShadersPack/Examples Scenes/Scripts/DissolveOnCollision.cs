using UnityEngine;
using System.Collections;

public class DissolveOnCollision : MonoBehaviour {
	public Shader dissolveShader;
	public Texture2D dissolvePattern;
	public Color dissolveEmissionColor;
	public float dissolveSpeed = 0.1f;
	public bool checkForTag = false;
	public string objectTag;
	Transform collidedObject;
	float sliceAmount;
	bool dissolve = false;
	
	bool collided;
	
	void Start(){
	
	}
	
	void Update () {		
			if(collided){
				collidedObject.renderer.material.shader = dissolveShader;
			    collidedObject.renderer.material.SetColor("_DissolveEmissionColor", dissolveEmissionColor);
				collidedObject.renderer.material.SetFloat("_DissolveEmissionThickness", -0.05f);
				collidedObject.renderer.material.SetTexture("_DissolveTex", dissolvePattern);
				dissolve = true;
			}
		
		if(dissolve){
			sliceAmount -= Time.deltaTime * dissolveSpeed;
			collidedObject.renderer.material.SetFloat("_DissolvePower", 0.65f + Mathf.Sin(0.9f)*sliceAmount);
			if(collidedObject.renderer.material.GetFloat("_DissolvePower") < -0.1f){
				//Destroy(collidedObject.gameObject);
				collidedObject.collider.enabled = false;
				Destroy(gameObject);
			}
		}
	}
	
	void OnTriggerEnter(Collider other){
		if(checkForTag){
			if(other.transform.tag == objectTag){
				collided = true;
				collidedObject = other.transform;
				transform.renderer.enabled = false;
				other.transform.tag = null;
				collider.enabled = false;
			}else{
				//Destroy(gameObject);
			}
		}else{
			collided = true;	
			collidedObject = other.transform;
		}
	}
}
