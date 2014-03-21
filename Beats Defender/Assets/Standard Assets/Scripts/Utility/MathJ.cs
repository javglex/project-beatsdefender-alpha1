using UnityEngine;
using System.Collections;

public class MathJ : MonoBehaviour {

	//compares two float values if they are equal, with a treshold
	public static bool Approx(float a, float b, float treshold){
		
		return (Mathf.Abs(a-b)<treshold);
		
	}
	
	//compares two float values if they are equal, with a percent treshold
	public static bool ApproxPer(float a, float b, float treshold){			//treshold value is now in percent form, so 1% = .01;
		
		return (Mathf.Abs(a-b)<((a+b)/2)*(treshold*.01f));			
		
	}
	
	public static bool ApproxQuaternion(Quaternion a, Quaternion b, float treshold){
			
			if (Mathf.Abs(a.x-b.x)<treshold&&Mathf.Abs(a.y-b.y)<treshold&&Mathf.Abs(a.z-b.z)<treshold&&Mathf.Abs(a.w-b.w)<treshold)
				return true;
				else return false;
		
	}
	
	public static bool ApproxVector3(Vector3 a, Vector3 b, float treshold){
			
		if (Mathf.Abs(a.x-b.x)<treshold&&Mathf.Abs(a.y-b.y)<treshold&&Mathf.Abs(a.z-b.z)<treshold)
			return true;
			else return false;
		
	}
	
	public static void RepeatInt(ref int val, int min, int max){
		if (val>max)
			val=min;
		if (val<min)
			val=max;
			
	}
}
