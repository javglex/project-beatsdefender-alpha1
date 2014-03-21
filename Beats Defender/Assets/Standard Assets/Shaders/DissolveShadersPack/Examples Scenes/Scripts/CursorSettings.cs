using UnityEngine;
using System.Collections;

public class CursorSettings : MonoBehaviour {
	void Start(){
		Screen.showCursor = false;
		Screen.lockCursor = true;
	}
}
