using UnityEngine;
using System.Collections;

public class DissolveTextureChange : MonoBehaviour {
	public Texture2D mainTexNormal;
	public Texture2D secondTexNormal;
	
	Color dissolveEmissionColor;
	float dissolveEmissionThickness = -0.02f;
	float dissolvePower = 0.6f;
	bool mainNormal;
	bool secNormal;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void Update(){
		renderer.material.SetColor("_DissolveEmissionColor", dissolveEmissionColor);	
		renderer.material.SetFloat("_DissolveEmissionThickness", dissolveEmissionThickness);
		renderer.material.SetFloat("_DissolvePower", dissolvePower);
		
		if(mainNormal)
			renderer.material.SetTexture("_MainTexNormal", mainTexNormal);
		else
			renderer.material.SetTexture("_MainTexNormal", null);
		
		if(secNormal)
			renderer.material.SetTexture("_SecondTexNormal", secondTexNormal);
		else
			renderer.material.SetTexture("_SecondTexNormal", null);
	}
	
	void OnGUI(){
		GUI.Label(new Rect(10, 40, 200, 20), "Dissolve Power");
		dissolvePower = GUI.HorizontalSlider(new  Rect(10, 60, 200, 20), dissolvePower, 0.6f, -0.2f);
		GUI.Label(new Rect(10, 80, 200, 20), "Dissolve Emission Thickness");
		dissolveEmissionThickness = GUI.HorizontalSlider(new Rect(10, 100, 200, 20), dissolveEmissionThickness, -0.01f, -0.026f);
		GUI.Label(new Rect(10, 120, 200, 20), "Dissolve Emission Color");
		dissolveEmissionColor.r = GUI.HorizontalSlider(new Rect(10, 140, 200, 20), dissolveEmissionColor.r, 0.0f, 1.0f);
		dissolveEmissionColor.g = GUI.HorizontalSlider(new Rect(10, 160, 200, 20), dissolveEmissionColor.g, 0.0f, 1.0f);
		dissolveEmissionColor.b = GUI.HorizontalSlider(new Rect(10, 180, 200, 20), dissolveEmissionColor.b, 0.0f, 1.0f);
	
	    mainNormal = GUI.Toggle(new Rect(10, 200, 200, 20), mainNormal, "Main texture normal map");
		secNormal = GUI.Toggle(new Rect(10, 220, 200, 20), secNormal, "Second texture normal map");
	}
}
