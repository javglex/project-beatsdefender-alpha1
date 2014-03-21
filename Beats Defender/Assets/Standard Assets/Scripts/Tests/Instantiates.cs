using UnityEngine;
using System.Collections;

public class Instantiates : MonoBehaviour {
	public GameObject enemy;
	static public int i=0;
	// Use this for initialization
	void Start () {
		InvokeRepeating("Spawn",1,.2f);
	}
	
	void Spawn(){
		if (i<22){
			GameObject enem= ObjectPoolManager.CreatePooled(enemy,transform.position,Quaternion.identity);
			enem.transform.parent=this.transform;
			i++;
		}
	}
	// Update is called once per frame
	public GUIText display; // drag a GUIText here to show results
	void Update(){
		if (display){ 
			display.text = i.ToString("F2");
		}
		
	}
}
