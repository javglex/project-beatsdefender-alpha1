using UnityEngine;
using System.Collections;
/*same as scrolltex but instead of diffuse texture it scrolls bump map*/
public class ScrollBump : MonoBehaviour {
	public float scrollSpeed = .01f;
	private float offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		offset += Time.deltaTime * scrollSpeed*Mathf.Sin(Time.time) ;
    	renderer.material.SetTextureOffset ("_NormalMap", new Vector2(0,offset*.5f));
	}
}
