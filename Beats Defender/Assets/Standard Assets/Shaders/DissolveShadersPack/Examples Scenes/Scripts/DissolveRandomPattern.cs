using UnityEngine;
using System.Collections;

public class DissolveRandomPattern : MonoBehaviour {
	void Awake(){
		transform.renderer.material.SetTextureOffset("_DissolveTex", new Vector2(Random.Range(1.0f, 10.0f), Random.Range(1.0f, 10.0f)));
	}
}
