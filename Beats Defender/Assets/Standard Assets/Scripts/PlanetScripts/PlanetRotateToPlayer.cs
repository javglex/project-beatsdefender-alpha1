using UnityEngine;
using System.Collections;

public class PlanetRotateToPlayer : MonoBehaviour {
	private GameObject planet;
	// Use this for initialization
	void Start () {
		planet=GameObject.FindWithTag("Planet");

	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(planet.transform);
	}
}
