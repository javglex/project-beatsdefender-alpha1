using UnityEngine;
using System.Collections;

public class AnalyzeFrequencies : MonoBehaviour {
	
	
	public struct FreqLimits{
		private int min;
		private int max;
		private float maxVol;
		private float vol;
		public FreqLimits(int mn, int mx){//constructor
			min=mn;
			max=mx;
			maxVol=0;
			vol=0;
		}
		public bool testFreq(float freq){
			if (freq>=min&&freq<=max)
				return true;
			else return false;
		}
		public bool testVol(float currVol, float sensitivity){		//sensitivity is by how much (in percentage) maxvol should decrement after reaching a peak
			vol=currVol;
			if (vol >= maxVol) {
				maxVol = vol;
				maxVol *= sensitivity*.01f;	
				return true;
			}
			else{
				maxVol-=Time.deltaTime;
				return false;
			}
		}
		
		public float getVolume(){		//returns volume, to see how instense a certain frequency is
			return vol;
		}

	}


	FreqLimits noLimits = new FreqLimits (0, 15000);
	static public FreqLimits bassLimits=new FreqLimits(60,113);
	FreqLimits lowLimits= new FreqLimits(113,450);
	FreqLimits medLimits= new FreqLimits(450,1800);
	FreqLimits highLimits= new FreqLimits(1800,3600);
	FreqLimits peakLimits= new FreqLimits(3600,15000);
	
	
	//public static float bassAmmt;
	private float freq=AnalyzeMusic.pitchValue;
	private float decibs=AnalyzeMusic.rmsValue;

	public GameObject[] cube;

	void Start(){
		
		
	}
	
	
	void FixedUpdate () {
		freq=AnalyzeMusic.pitchValue+.05f;		//so it never equals 0
		decibs=AnalyzeMusic.rmsValue*10;
		BeatDetectVol();
		VolumeCheck();		//find average volume level;
		BeatDetect ();
		BeatsPerMinute();	//find beats per minute of song
		KickDetect ();
		SnareDetect ();
	}

	//private int beatCount=0;
	bool BeatDetectVol(){	//detect beat by vol, all freqs, called in BPM function
		cube[0].transform.localScale=Vector3.Slerp (cube[0].transform.localScale,new Vector3(1,1,1), 15*Time.deltaTime);
		if (noLimits.testFreq(freq)&&noLimits.testVol(decibs,90)){
			cube[0].transform.localScale= new Vector3(2,2,2);
			beatCount++;
			return true;
		}else return false;


	}

	bool BeatDetect(){
		cube[1].transform.localScale=Vector3.Slerp (cube[1].transform.localScale,new Vector3(1,1,1), 15*Time.deltaTime);
		if (bassLimits.testFreq(freq)&&bassLimits.testVol(decibs,85)){
			cube[1].transform.localScale= new Vector3(2,2,2);
			
			return true;
		}else return false;

	}
	bool KickDetect(){
		cube[2].transform.localScale=Vector3.Slerp (cube[2].transform.localScale,new Vector3(1,1,1), 15*Time.deltaTime);
		if (lowLimits.testFreq(freq)&&lowLimits.testVol(decibs,85)){
			cube[2].transform.localScale= new Vector3(2,2,2);
			
			return true;
		}else return false;
		
		
	}
	bool SnareDetect(){
		cube[3].transform.localScale=Vector3.Slerp (cube[3].transform.localScale,new Vector3(1,1,1), 15*Time.deltaTime);
		if (medLimits.testFreq(freq)&&medLimits.testVol(decibs,70)){
			cube[3].transform.localScale= new Vector3(2,2,2);
			return true;
		}else return false;
		
		
	}

	private int beatCount=1;	//number of beats
	private float timeLapsed; //how much time has passed
	private float beatsPerMin;		//the number of beats per minute! adouy!
	public static float tempo=0;
	public ParticleEmitter test;	//a test
	void BeatsPerMinute(){

		timeLapsed += Time.deltaTime;
		if (timeLapsed >= .5f) {		//if second passed
			beatsPerMin=beatCount*2;
			beatCount=1;
			timeLapsed=0; //reset
		}
		tempo=Mathf.Lerp(tempo,(beatsPerMin/360)*(decibs*2),Time.deltaTime*100f);
		test.worldVelocity=new Vector3(0f,0f,-1.5f-tempo);
//		Debug.Log ("BPM: " + beatsPerMin+ "decibs: "+decibs);
		
	}


	
	
	
	
	/*Vol check variables*/
	private int count=1;	//divide total volume by this to get average
	private float totalVol=0;
	private float averageVol;	//average volume level

	void VolumeCheck()
	{
		if (count>10000){		//reset
			count=1;
			totalVol=0;
		}
		totalVol+=decibs;
		averageVol=totalVol/count;
		count++;

		//Debug.Log ("average volume: " + averageVol+ "Count: " +count);
	}

}