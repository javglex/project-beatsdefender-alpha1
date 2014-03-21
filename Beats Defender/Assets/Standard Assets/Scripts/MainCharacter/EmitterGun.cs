using UnityEngine;
using System.Collections;

public class EmitterGun : MonoBehaviour {

	private Color[] modifiedColors;
	private ParticleAnimator particleAnimator;
	private float playerTranslateSpeed=0;
	public LensFlare lf;

	private float speedAdd=0;		//intense music=more particle speed
	
	
   void Start() {
        particleAnimator = GetComponent<ParticleAnimator>();
        modifiedColors = particleAnimator.colorAnimation;
        modifiedColors[0] = Color.cyan;
		lf.color=Color.cyan;
        particleAnimator.colorAnimation = modifiedColors;

		
    }

	void Update(){

		Run();
		
	}



	void Run(){

//		playerTranslateSpeed=PlayerMovement.speedMagnitude;
//		speedAdd=Mathf.Lerp(speedAdd,ReadFrequencies.temp/2,2*Time.deltaTime); /*commented out because temp currently unavailable*/
		float totalSpeed=21*speedAdd;
		totalSpeed=Mathf.Clamp(totalSpeed,6,45);
	//	particleEmitter.minSize=particleEmitter.maxSize=ReadFrequencies.temp/2;
		particleEmitter.localVelocity=new Vector3(totalSpeed,0,0);

		particleAnimator.localRotationAxis=new Vector3(0,((particleEmitter.localVelocity.x*2)/WorldSystem.planetRadius),0);
		//particleEmitter.localVelocity=new Vector3(Mathf.Clamp(particleEmitter.localVelocity.x,5,8),0,0);
		
		
		
	//	if (ReadFrequencies.temp>4.8f){
			modifiedColors[0] = Color.red;

//		}else
		{

			modifiedColors[0] = Color.cyan;
		}
		
		
		particleAnimator.colorAnimation = modifiedColors;
	}
	


	
	
	
}
