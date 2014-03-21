using UnityEngine;
using System.Collections;

public class Transformer : MonoBehaviour {



public int i;
static public float[] amp= new float[8];
private float intensity=0;
public bool isLight;
public bool isFlare;
public float ammt=1;			//intensity (low if outside, high if inside for better fx)
public LensFlare flare;

	
void Update () {


    if (isLight)
    {
    	transformLight();
    }
	if (isFlare)
    {
    	transformFlare();
    }

}



public Light light1=null;

void transformLight()
	{	
		//intensity=(1/(AnalyzeMusic.pitchValue+.1f))*50*(AnalyzeMusic.rmsValue);

		//light1.intensity=Mathf.Lerp(light1.intensity,.01f+((amp[i]/5.14f)*ammt),25f*Time.deltaTime);
		//light1.intensity=Mathf.Clamp(light1.intensity,.01f,5);

	}
void transformFlare(){
			
		flare.brightness=.01f+AnalyzeFrequencies.bassLimits.getVolume()/20f;
	}
}
