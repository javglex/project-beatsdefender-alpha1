using UnityEngine;
using System.Collections;

public class MeteorSpawn : MonoBehaviour {

	private float random;
	public NewObjectPool objectPool;
	// Use this for initialization
	void Start () {
		InvokeRepeating("Spawn",1,.5f);
	}
	void Spawn(){
		random=Random.Range(-3f,3f);
		objectPool.ActivateMeteor(new Vector2(random,6));
	}


}
