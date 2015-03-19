// Upgrade NOTE: commented out 'float4 unity_ShadowFadeCenterAndType', a built-in variable

Shader "Linear Lighting/Transparent/Specular" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_MainTex ("Base (RGB) TransGloss (A)", 2D) = "white" {}
}
SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 300

	Alphatest Greater 0 ZWrite Off ColorMask RGB
	
Pass {
		Name "FORWARD"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha
CGPROGRAM
#pragma target 3.0
#include "LinLighting.cginc"
#pragma vertex vert_surf
#pragma fragment frag_surf
#pragma fragmentoption ARB_precision_hint_fastest
#pragma multi_compile_fwdbasealpha
#include "HLSLSupport.cginc"
#define UNITY_PASS_FORWARDBASE
#include "UnityCGGoLin.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"

#define INTERNAL_DATA
#define WorldReflectionVector(data,normal) data.worldRefl
#define WorldNormalVector(data,normal) normal
#line 1
#line 13

#pragma debug
//#pragma surface surf BlinnPhong alpha

sampler2D _MainTex;
fixed4 _Color;
half _Shininess;

struct Input {
	float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 tex = LLDecodeTex( tex2D(_MainTex, IN.uv_MainTex) );
	o.Albedo = tex.rgb * LLDecodeGamma( _Color.rgb );
	o.Gloss = tex.a;
	o.Alpha = tex.a * LLDecodeGamma( _Color.a );
	o.Specular = _Shininess;
}
#ifdef LIGHTMAP_OFF
struct v2f_surf {
  float4 pos : SV_POSITION;
  float2 pack0 : TEXCOORD0;
  fixed3 normal : TEXCOORD1;
  fixed3 vlight : TEXCOORD2;
  float3 viewDir : TEXCOORD3;
  LIGHTING_COORDS(4,5)
};
#endif
#ifndef LIGHTMAP_OFF
struct v2f_surf {
  float4 pos : SV_POSITION;
  float2 pack0 : TEXCOORD0;
  float2 lmap : TEXCOORD1;
#ifndef DIRLIGHTMAP_OFF
  float3 viewDir : TEXCOORD2;
  LIGHTING_COORDS(3,4)
#else
  LIGHTING_COORDS(2,3)
#endif
};
#endif
#ifndef LIGHTMAP_OFF
float4 unity_LightmapST;
// float4 unity_ShadowFadeCenterAndType;
#endif
float4 _MainTex_ST;
v2f_surf vert_surf (appdata_full v) {
  v2f_surf o;
  o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
  o.pack0.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
  #ifndef LIGHTMAP_OFF
  o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
  #endif
  float3 worldN = mul((float3x3)_Object2World, SCALED_NORMAL);
  #ifdef LIGHTMAP_OFF
  o.normal = worldN;
  #endif
  #ifndef DIRLIGHTMAP_OFF
  TANGENT_SPACE_ROTATION;
  float3 viewDirForLight = mul (rotation, ObjSpaceViewDir(v.vertex));
  o.viewDir = viewDirForLight;
  #else
  #ifdef LIGHTMAP_OFF
  float3 viewDirForLight = WorldSpaceViewDir( v.vertex );
  o.viewDir = viewDirForLight;
  #endif
  #endif
  #ifdef LIGHTMAP_OFF
  float3 shlight = ShadeSH9 (float4(worldN,1.0));
  o.vlight = shlight;
  #ifdef VERTEXLIGHT_ON
  float3 worldPos = mul(_Object2World, v.vertex).xyz;
  o.vlight += Shade4PointLights (
    unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
    unity_LightColor[0].rgb, unity_LightColor[1].rgb, unity_LightColor[2].rgb, unity_LightColor[3].rgb,
    unity_4LightAtten0, worldPos, worldN );
  #endif // VERTEXLIGHT_ON
  #endif // LIGHTMAP_OFF
  TRANSFER_VERTEX_TO_FRAGMENT(o);
  return o;
}
#ifndef LIGHTMAP_OFF
sampler2D unity_Lightmap;
#ifndef DIRLIGHTMAP_OFF
sampler2D unity_LightmapInd;
#endif
#endif
fixed4 frag_surf (v2f_surf IN) : COLOR {
  Input surfIN;
  surfIN.uv_MainTex = IN.pack0.xy;
  SurfaceOutput o;
  o.Albedo = 0.0;
  o.Emission = 0.0;
  o.Specular = 0.0;
  o.Alpha = 0.0;
  o.Gloss = 0.0;
  #ifdef LIGHTMAP_OFF
  o.Normal = IN.normal;
  #endif
  surf (surfIN, o);
  fixed atten = LIGHT_ATTENUATION(IN);
  fixed4 c = 0;
  #ifdef LIGHTMAP_OFF
  c = LightingBlinnPhong (o, _WorldSpaceLightPos0.xyz, normalize(half3(IN.viewDir)), atten);
  #endif // LIGHTMAP_OFF
  #ifdef LIGHTMAP_OFF
  c.rgb += o.Albedo * IN.vlight;
  #endif // LIGHTMAP_OFF
  #ifndef LIGHTMAP_OFF
  #ifdef DIRLIGHTMAP_OFF
  fixed4 lmtex = tex2D(unity_Lightmap, IN.lmap.xy);
  fixed3 lm = DecodeLightmap (lmtex);
  #else
  o.Normal = half3(0,0,1);
  half3 specColor;
  fixed4 lmtex = tex2D(unity_Lightmap, IN.lmap.xy);
  fixed4 lmIndTex = tex2D(unity_LightmapInd, IN.lmap.xy);
  half3 lm = LightingBlinnPhong_DirLightmap(o, lmtex, lmIndTex, normalize(half3(IN.viewDir)), 0, specColor).rgb;
  c.rgb += specColor;
  #endif
  #ifdef SHADOWS_SCREEN
  #if defined(SHADER_API_GLES) && defined(SHADER_API_MOBILE)
  c.rgb += o.Albedo * min(lm, atten*2);
  #else
  c.rgb += o.Albedo * max(min(lm,(atten*2)*lmtex.rgb), lm*atten);
  #endif
  #else // SHADOWS_SCREEN
  c.rgb += o.Albedo * lm;
  #endif // SHADOWS_SCREEN
  c.a = o.Alpha;
#endif // LIGHTMAP_OFF
  c.a = o.Alpha;
  return LLEncodeGamma( c );
}

ENDCG
}
Pass {
		Name "FORWARD"
		Tags { "LightMode" = "ForwardAdd" }
		ZWrite Off Blend One One Fog { Color (0,0,0,0) }
		Blend SrcAlpha One
CGPROGRAM
#pragma target 3.0
#include "LinLighting.cginc"
#pragma vertex vert_surf
#pragma fragment frag_surf
#pragma fragmentoption ARB_precision_hint_fastest
#pragma multi_compile_fwdadd
#include "HLSLSupport.cginc"
#define UNITY_PASS_FORWARDADD
#include "UnityCGGoLin.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"

#define INTERNAL_DATA
#define WorldReflectionVector(data,normal) data.worldRefl
#define WorldNormalVector(data,normal) normal
#line 1
#line 13

#pragma debug
//#pragma surface surf BlinnPhong alpha

sampler2D _MainTex;
fixed4 _Color;
half _Shininess;

struct Input {
	float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 tex = LLDecodeTex( tex2D(_MainTex, IN.uv_MainTex) );
	o.Albedo = tex.rgb * LLDecodeGamma( _Color.rgb );
	o.Gloss = tex.a;
	o.Alpha = tex.a * LLDecodeGamma( _Color.a );
	o.Specular = _Shininess;
}
struct v2f_surf {
  float4 pos : SV_POSITION;
  float2 pack0 : TEXCOORD0;
  fixed3 normal : TEXCOORD1;
  half3 lightDir : TEXCOORD2;
  half3 viewDir : TEXCOORD3;
  LIGHTING_COORDS(4,5)
};
float4 _MainTex_ST;
v2f_surf vert_surf (appdata_full v) {
  v2f_surf o;
  o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
  o.pack0.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
  o.normal = mul((float3x3)_Object2World, SCALED_NORMAL);
  float3 lightDir = WorldSpaceLightDir( v.vertex );
  o.lightDir = lightDir;
  float3 viewDirForLight = WorldSpaceViewDir( v.vertex );
  o.viewDir = viewDirForLight;
  TRANSFER_VERTEX_TO_FRAGMENT(o);
  return o;
}
fixed4 frag_surf (v2f_surf IN) : COLOR {
  Input surfIN;
  surfIN.uv_MainTex = IN.pack0.xy;
  SurfaceOutput o;
  o.Albedo = 0.0;
  o.Emission = 0.0;
  o.Specular = 0.0;
  o.Alpha = 0.0;
  o.Gloss = 0.0;
  o.Normal = IN.normal;
  surf (surfIN, o);
  #ifndef USING_DIRECTIONAL_LIGHT
  fixed3 lightDir = normalize(IN.lightDir);
  #else
  fixed3 lightDir = IN.lightDir;
  #endif
  fixed4 c = LightingBlinnPhong (o, lightDir, normalize(half3(IN.viewDir)), LIGHT_ATTENUATION(IN));
  c.a = o.Alpha;
  return LLEncodeGamma( c );
}

ENDCG
}
}
Fallback "Linear Lighting/Transparent/VertexLit CG"
}
