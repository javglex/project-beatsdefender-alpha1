using UnityEngine;
using System.Collections;

public class MusicSelectTest : MonoBehaviour {
	
	
	public AudioClip[] music;
	// Use this for initialization
	void Start () {
		 audio.clip = music[0];
		audio.Play();
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (GUILayout.Button("Music 1",GUILayout.MinHeight(50))){
			audio.clip = music[0];
			audio.Play();
		}
		if (GUILayout.Button("Music 2",GUILayout.MinHeight(50))){
			audio.clip = music[1];
			audio.Play();
		}
		if (GUILayout.Button("Music 3",GUILayout.MinHeight(50))){
			audio.clip = music[2];
			audio.Play();
		}
		if (GUILayout.Button("Music 4",GUILayout.MinHeight(50))){
			audio.clip = music[3];
			audio.Play();
		}
	}
}
