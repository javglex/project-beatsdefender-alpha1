//Wrote by Andrew M.

Shader "CBG/Dissolve/Dissolve" 
{

  Properties {
    _Color("Color", Color) = (1,1,1,1)
    _DissolvePower ("Dissolve Power", Range(0.65, -0.5)) = 0
    _DissolveEmissionThickness ("Dissolve Emission Thickness", Range(-0.02, -0.05)) = 0.03
    _DissolveEmissionColor ("Dissolve Emission Color", Color) = (1,1,1)
    _Shininess ("Shininess", Range(0.1, 1)) = 0.02
    _SpecularColor ("Specular Color", Color) = (1,1,1,1)
    _Emission ("Emission", Range(0, 1)) = 0
    _ParallaxPower ("Height", Range (0.005, 0.08)) = 0.02
    _MainTex ("Main Texture", 2D) = "white"{}
    _NormalMap("Normal Map", 2D) = "bump"{}
    _ParallaxMap ("Parralax Map", 2D) = "black"{}
    _LightMap ("Light Map", 2D) = "black"{}
    _DissolveTex ("Dissolve Texture", 2D) = "white"{}
    _DetailTex ("Detail Texture", 2D) = "white"{}
    _MaskTex ("Alpha Mask", 2D) = "white"{}
  }

  SubShader 
  {
  
    Tags {"IgnoreProjector"="True" "RenderType"="TransparentCutout"}
    LOD 300 
    Cull Off

    CGPROGRAM
    #pragma surface surf SimpleSpecular alphatest:Zero
    #pragma target 3.0
    
    fixed4 _Color;
    fixed _DissolvePower;
    fixed _DissolveEmissionThickness;
    float3 _DissolveEmissionColor;
    fixed _Shininess;
    fixed4 _SpecularColor;
    fixed _Emission;
    float _ParallaxPower;
    sampler2D _MainTex;
    sampler2D _NormalMap;
    sampler2D _ParallaxMap;
    sampler2D _LightMap;
    sampler2D _DissolveTex;
    sampler2D _DetailTex;
    sampler2D _MaskTex;

   struct Input {
    float2 uv_MainTex;
    float2 uv_NormalMap;
    float2 uv2_LightMap;
    float2 uv_DissolveTex;
    float2 uv_DetailTex;
    float2 uv_MaskTex;
    float3 worldPos;
   };

   half4 LightingSimpleSpecular (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) 
   {
     half3 h = normalize (lightDir + viewDir);

     half diff = max (0, dot (s.Normal, lightDir));

     float nh = max (0, dot (s.Normal, h));
     float spec = pow (nh, 48.0);

     half4 c;
     c.rgb = (s.Albedo * _LightColor0.rgb * diff + _SpecularColor.rgb * spec * _Shininess) * (atten * 2);
     c.a = s.Alpha;
     return c;
   }

   void surf (Input IN, inout SurfaceOutput o) 
   {
     half4 tex = tex2D(_MainTex, IN.uv_MainTex);
     half4 texd = tex2D(_DissolveTex, IN.uv_DissolveTex);
     half4 texdetail = tex2D(_DetailTex, IN.uv_DetailTex);  
     half4 maskTex = tex2D(_MaskTex, IN.uv_MaskTex);
     
     half h = tex2D(_ParallaxMap, IN.uv_NormalMap).w;
	 float2 offset = ParallaxOffset(h, _ParallaxPower, IN.worldPos);
	 IN.uv_MainTex += offset;
	 IN.uv_NormalMap += offset;
     half4 lm = tex2D (_LightMap, IN.uv2_LightMap);     
     
     o.Albedo = tex.rgb * _Color;
     o.Albedo *= texdetail.rgb; 
     o.Gloss = tex.a;   
     if(maskTex.a > 0.3)
       o.Alpha = _DissolvePower - texd.r;
     o.Emission = lm.rgb * o.Albedo.rgb;
     o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
     
     if ((o.Alpha < 0)&&(o.Alpha > _DissolveEmissionThickness/3)){
       o.Alpha = 1;
       o.Albedo = _DissolveEmissionColor;
       o.Emission = _DissolveEmissionColor * _Emission; 
     }          
     if ((o.Alpha < _DissolveEmissionThickness/3)&&(o.Alpha > _DissolveEmissionThickness/2)){
       o.Alpha = 1;
       o.Albedo = _DissolveEmissionColor * 1.5;
     }
     if ((o.Alpha < _DissolveEmissionThickness/2)&&(o.Alpha > _DissolveEmissionThickness)){
       o.Alpha = 1;
       o.Albedo = (0,0,0,0);
     }
   } 
   ENDCG
  }
}