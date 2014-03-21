//Wrote by Andrew M.

Shader "CBG/Dissolve/DissolveNormalSpecular" 
{

  Properties {
    _DissolvePower ("Dissolve Power", Range(0.65, -0.5)) = 0.2
    _DissolveEmissionThickness ("Dissolve Emission Thickness", Range(-0.02, -0.05)) = -0.03
    _DissolveEmissionColor ("Dissolve Emission Color", Color) = (1,1,1)
    _Shininess ("Shininess", Range(0.01, 1)) = 0.02
    _SpecularColor ("Specular Color", Color) = (1,1,1,1)
    _MainTex ("Main Texture", 2D) = "white"{}
    _NormalMap("Normal Map", 2D) = "bump"{}
    _DissolveTex ("Dissolve Texture", 2D) = "white"{}    
  }

  SubShader 
  {
  
    Tags {"IgnoreProjector"="True" "RenderType"="TransparentCutout"}
    LOD 300 
    Cull Off

    CGPROGRAM
    #pragma surface surf SimpleSpecular alphatest:Zero
    #pragma target 3.0
    
    sampler2D _MainTex;
    sampler2D _DissolveTex;
    sampler2D _NormalMap;
    fixed4 _DissolveEmissionColor;
    float3 _DissolveEmission;
    fixed _DissolvePower;
    fixed _DissolveEmissionThickness;
    fixed _Shininess;
    fixed4 _SpecularColor;

   struct Input {
    float2 uv_MainTex;
    float2 uv_DissolveTex;
    float2 uv_NormalMap;
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
     
     o.Albedo = tex.rgb;
     o.Gloss = tex.a;
     o.Alpha = _DissolvePower - texd.r;
     o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
     
     if ((o.Alpha < 0)&&(o.Alpha > _DissolveEmissionThickness/3)){
       o.Alpha = 1;
       o.Albedo = _DissolveEmissionColor;
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