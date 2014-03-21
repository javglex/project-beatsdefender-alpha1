using UnityEngine;
using System.Collections;

public class DissolveOnClick : MonoBehaviour {
	public Shader dissolveShader;
	public Texture2D dissolvePattern;
	public Color dissolveEmissionColor;
	public float dissolveSpeed = 0.1f;
	float sliceAmount;
	bool dissolve = false;
		
	bool mouseOver;
	
	void Update () {		
		if(mouseOver){
			if(Input.GetMouseButtonUp(0)){
				transform.renderer.material.shader = dissolveShader;
				transform.renderer.material.SetColor("_DissolveEmissionColor", dissolveEmissionColor);
				transform.renderer.material.SetFloat("_DissolveEmissionThickness", -0.05f);
				transform.renderer.material.SetTexture("_DissolveTex", dissolvePattern);
				transform.renderer.material.SetTextureOffset("_DissolveTex", new Vector2(Random.Range(1.0f, 10.0f), Random.Range(1.0f, 10.0f)));
				dissolve = true;
			}
		}
		
		if(dissolve){
			sliceAmount -= Time.deltaTime * dissolveSpeed;
			transform.renderer.material.SetFloat("_DissolvePower", 0.65f + Mathf.Sin(0.9f)*sliceAmount);
     		if(renderer.material.GetFloat("_DissolvePower") < 0.2f)
				dissolve = false;
		}
	}
	
	
	void OnMouseEnter(){
		mouseOver = true;
	}
	void OnMouseExit(){
		mouseOver = false;
	}
}
