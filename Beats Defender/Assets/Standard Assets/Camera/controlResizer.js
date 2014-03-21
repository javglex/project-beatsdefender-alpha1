




var gui:GUITexture;		//left
var gui2:GUITexture;		//right

var sit1:GUITexture;
var sit2:GUITexture;



var JoyX:float;
var JoyY:float;

var JoyXX:float;
var JoyYY:float;


var OutX:float;
var OutY:float;
var OutXX:float;
var OutYY:float;

private var winScale:double=1;

private var width:float=Screen.width;
private var height:float=Screen.height;






function Awake () {
width=Screen.width;
height=Screen.height;



winScale=((width-40)/800f);

//	Debug.Log(winScale);


	//joystick
    gui2.pixelInset.x =JoyX*winScale;
    gui2.pixelInset.y =JoyY*winScale;
  
    
    gui.pixelInset.x =  JoyXX*winScale;
    gui.pixelInset.y = JoyYY*winScale;
    
     //joystick holders  
    sit1.pixelInset.x = OutX*(winScale);
    sit1.pixelInset.y =OutY*(winScale);
  
    
    sit2.pixelInset.x =OutXX*(winScale);
    sit2.pixelInset.y =OutYY*(winScale);
      
          
}

