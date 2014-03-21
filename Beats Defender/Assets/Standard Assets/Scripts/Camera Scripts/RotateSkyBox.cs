using UnityEngine;

[RequireComponent(typeof(Skybox))]
public class RotateSkyBox : MonoBehaviour {
	
	
	Quaternion rot = Quaternion.Euler (60f, 0f, 0f);
    Matrix4x4 m = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3(1,1,1) );
	private int i=0;
    void Start () {
        // Construct a rotation matrix and set it for the shader

        GetComponent<Skybox>().material.SetMatrix ("_Rotation", m);
    }
	
	void Update(){
		i++;
		rot=Quaternion.Euler(0,i,i);
		m = Matrix4x4.TRS (Vector3.zero, rot, new Vector3(1,1,1) );
		GetComponent<Skybox>().material.SetMatrix ("_Rotation", m);
		
	}

}