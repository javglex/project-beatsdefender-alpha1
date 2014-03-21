using UnityEngine;
using System.Collections;

public class ScrollDownTexture : MonoBehaviour {

	public float scrollSpeed = .01f;
	public float offset;
	private Material[] mat;

	void Update () {

		Run();

	}


	void Run(){

     offset += Time.deltaTime * scrollSpeed ;
     renderer.material.SetTextureOffset ("_MainTex", new Vector2(offset,offset*.5f));


	}
}