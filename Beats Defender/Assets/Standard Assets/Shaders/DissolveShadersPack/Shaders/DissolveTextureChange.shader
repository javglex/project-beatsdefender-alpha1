//Wrote by Andrew M.

Shader "CBG/Dissolve/DissolveTextureChange" 
{

  Properties {
    _MTColor("Main Texture Color", Color) = (1,1,1,1)
    _STColor("Second Texture Color", Color) = (1,1,1,1)
    _DissolvePower ("Dissolve Power", Range(0.75, -0.2)) = 0
    _DissolveEmissionThickness ("Dissolve Emission Thickness", Range(-0.01, -0.026)) = -0.02
    _DissolveEmissionColor ("Dissolve Emission Color", Color) = (0, 0, 0, 0)
    _MainTex ("Main Texture", 2D) = "white"{}
    _MainTexNormal("Main Texture Bump", 2D) = "bump"{}
    _SecondTex ("Second Texture", 2D) = "white"{}
    _SecondTexNormal("Second Texture Bump", 2D) = "bump"{}
    _DissolveTex ("Dissolve Texture", 2D) = "white"{}
    _LightMap ("Light Map", 2D) = "black"{}
  }

  SubShader 
  {
  
    Tags {"IgnoreProjector"="True" "RenderType"="TransparentCutout"}
    LOD 200 
    
    CGPROGRAM
    #pragma surface surf Lambert alphatest:Zero
    #pragma target 3.0
    
    fixed4 _MTColor;
    fixed4 _STColor;
    fixed _DissolvePower;
    fixed _DissolveEmissionThickness;
    fixed4 _DissolveEmissionColor;
    sampler2D _MainTex;
    sampler2D _MainTexNormal;
    sampler2D _SecondTex;
    sampler2D _SecondTexNormal;
    sampler2D _DissolveTex;
    sampler2D _LightMap;

   struct Input 
   {
     float2 uv_MainTex;
     float2 uv_MainTexNormal;
     float2 uv_SecondTex;
     float2 uv_SecondTexNormal;
     float2 uv_DissolveTex;
     float2 uv2_LightMap;
   };
 
   void surf (Input IN, inout SurfaceOutput o)
   {     
     half4 tex = tex2D(_MainTex, IN.uv_MainTex);
     half4 stex = tex2D(_SecondTex, IN.uv_SecondTex);
     half4 texd = tex2D(_DissolveTex, IN.uv_DissolveTex);
     
     half4 lm = tex2D (_LightMap, IN.uv2_LightMap); 
     
     o.Albedo = tex.rgb * _MTColor;
     o.Gloss = tex.a;
     o.Normal = UnpackNormal(tex2D(_MainTexNormal, IN.uv_MainTexNormal));
     o.Emission = lm.rgb * o.Albedo.rgb;
     
     o.Alpha = _DissolvePower - texd.r;
     
     if ((o.Alpha < 0)&&(o.Alpha > _DissolveEmissionThickness))
     {
       o.Alpha = 1;
       o.Albedo = _DissolveEmissionColor;
     }
     if ((o.Alpha < _DissolveEmissionThickness)&&(o.Alpha > -1))
     {
       o.Alpha = 1;
       o.Albedo = (0,0,0,0) + stex.rgb * _STColor;
       o.Normal = UnpackNormal(tex2D(_SecondTexNormal, IN.uv_SecondTexNormal));       
     }
   }  
   ENDCG
  }
}