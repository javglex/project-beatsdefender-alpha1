using UnityEngine;
using System.Collections;

public class AnalyzeMusic : MonoBehaviour {
	
	
public int qSamples  = 1024;  // array size
public float refValue  = 0.1f; // RMS value for 0 dB
public float threshold = 0.02f;      // minimum amplitude to extract pitch
static public float rmsValue ;   // sound level - RMS
private float dbValue ;    // sound level - dB
static public float pitchValue ; // sound pitch - Hz

private float[] samples ; // audio samples
private float[] spectrum ; // audio spectrum
public int channel=0; //to determine which channel to read



void Start () {
	samples = new float[qSamples];
	spectrum = new float[qSamples];
	
}

	
void AnalyzeSound(){
	audio.GetOutputData(samples, 0); // fill array with samples

	int i ;

	float sum = 0;

	
	for ( i=0; i < qSamples; i++){
		sum += samples[i]*samples[i]; // sum squared samples, instant energy e
	}
	
	rmsValue = Mathf.Sqrt(sum/qSamples); // rms = square root of average
	
	dbValue = 20*Mathf.Log10(rmsValue/refValue); // calculate dB
	if (dbValue < -160) dbValue = -160; // clamp it to -160dB min
	// get sound spectrum
	audio.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular );
	float maxV = 0;
	int maxN  = 0;
	for (i=0; i < qSamples; i++){ // find max 
		if (spectrum[i] > maxV && spectrum[i] > threshold){
			maxV = spectrum[i];
			maxN = i; // maxN is the index of max
		}
	}
	float freqN = maxN; // pass the index to a float variable
	if (maxN > 0 && maxN < qSamples-1){ // interpolate index using neighbours
		var dL = spectrum[maxN-1]/spectrum[maxN];
		var dR = spectrum[maxN+1]/spectrum[maxN];
		freqN += 0.5f*(dR*dR - dL*dL);
	}
	pitchValue = freqN*24000/qSamples; // convert index to frequency
}

public GUIText display; // drag a GUIText here to show results


//private float channelRate = .01f;
//private float nextRate = 0.0f;


void FixedUpdate () {

    
	AnalyzeSound();
	if (display){ 
		display.text = "RMS: "+rmsValue.ToString("F2")+
		" ("+dbValue.ToString("F1")+" dB)\n"+
		"Pitch: "+pitchValue.ToString("F0")+
		" Hz\n"+channel.ToString("F0")+" Ch";
	}
	
	
	
	
}

}
