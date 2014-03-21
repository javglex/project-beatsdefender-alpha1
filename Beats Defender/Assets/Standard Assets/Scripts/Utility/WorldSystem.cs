using UnityEngine;
using System.Collections;

public class WorldSystem : MonoBehaviour {
	
	 private Transform planet;
	static public float planetRadius;
	// Use this for initialization
	void Start () {
		planet=GameObject.FindWithTag("PlanetRadius").transform;
		planetRadius=planet.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
