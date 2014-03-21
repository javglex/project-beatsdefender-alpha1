using UnityEngine;
using System.Collections;

public class NewObjectPool : MonoBehaviour {
	public GameObject meteorO;
	private GameObject[] meteors=null;
	private int numMeteorsInstantiate=10;
	// Use this for initialization
	void Start () {
		meteors=new GameObject[numMeteorsInstantiate];
		InstantiateMeteors ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void InstantiateMeteors(){
		for (int i=0;i<numMeteorsInstantiate;i++){
			meteors[i]=Instantiate (meteorO) as GameObject;
			meteors[i].transform.parent=this.transform;
			meteors[i].SetActive (false);
		}
	}
	
	public void ActivateMeteor(Vector2 pos){
		for (int i=0;i<numMeteorsInstantiate;i++){
			if (meteors[i].active==false){
				meteors[i].SetActive (true);
				meteors[i].GetComponent<MeteorO>().Activate(pos);
				return;
			}
			
		}
	}
}
