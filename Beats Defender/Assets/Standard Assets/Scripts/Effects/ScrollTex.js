var scrollSpeed : float = .01;
var offset : float;
var mat:Material[];

function Update () {


		Run();
	

}


function Run(){

     offset += Time.deltaTime * scrollSpeed*Mathf.Sin(Time.time) ;
    renderer.material.SetTextureOffset ("_MainTex", Vector2(0,offset*.5));


}