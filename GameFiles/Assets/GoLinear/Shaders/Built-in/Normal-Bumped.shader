// Upgrade NOTE: commented out 'float4 unity_ShadowFadeCenterAndType', a built-in variable

Shader "Linear Lighting/Bumped Diffuse" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
}
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 300

	
Pass {
		Name "FORWARD"
		Tags { "LightMode" = "ForwardBase" }
CGPROGRAM
#pragma target 3.0
#include "LinLighting.cginc"
#pragma vertex vert_surf
#pragma fragment frag_surf
#pragma fragmentoption ARB_precision_hint_fastest
#pragma multi_compile_fwdbase
#include "HLSLSupport.cginc"
#define UNITY_PASS_FORWARDBASE
#include "UnityCGGoLin.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"

#define INTERNAL_DATA
#define WorldReflectionVector(data,normal) data.worldRefl
#define WorldNormalVector(data,normal) normal
#line 1
#line 12

#pragma debug
//#pragma surface surf Lambert

sampler2D _MainTex;
sampler2D _BumpMap;
fixed4 _Color;

struct Input {
	float2 uv_MainTex;
	float2 uv_BumpMap;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = LLDecodeTex( tex2D(_MainTex, IN.uv_MainTex) ) * LLDecodeGamma( _Color );
	o.Albedo = c.rgb;
	o.Alpha = c.a;
	o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
}
#ifdef LIGHTMAP_OFF
struct v2f_surf {
  float4 pos : SV_POSITION;
  float4 pack0 : TEXCOORD0;
  fixed3 lightDir : TEXCOORD1;
  fixed3 vlight : TEXCOORD2;
  LIGHTING_COORDS(3,4)
};
#endif
#ifndef LIGHTMAP_OFF
struct v2f_surf {
  float4 pos : SV_POSITION;
  float4 pack0 : TEXCOORD0;
  float2 lmap : TEXCOORD1;
  LIGHTING_COORDS(2,3)
};
#endif
#ifndef LIGHTMAP_OFF
float4 unity_LightmapST;
// float4 unity_ShadowFadeCenterAndType;
#endif
float4 _MainTex_ST;
float4 _BumpMap_ST;
v2f_surf vert_surf (appdata_full v) {
  v2f_surf o;
  o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
  o.pack0.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
  o.pack0.zw = TRANSFORM_TEX(v.texcoord, _BumpMap);
  #ifndef LIGHTMAP_OFF
  o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
  #endif
  float3 worldN = mul((float3x3)_Object2World, SCALED_NORMAL);
  TANGENT_SPACE_ROTATION;
  float3 lightDir = mul (rotation, ObjSpaceLightDir(v.vertex));
  #ifdef LIGHTMAP_OFF
  o.lightDir = lightDir;
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
  surfIN.uv_BumpMap = IN.pack0.zw;
  SurfaceOutput o;
  o.Albedo = 0.0;
  o.Emission = 0.0;
  o.Specular = 0.0;
  o.Alpha = 0.0;
  o.Gloss = 0.0;
  surf (surfIN, o);
  fixed atten = LIGHT_ATTENUATION(IN);
  fixed4 c = 0;
  #ifdef LIGHTMAP_OFF
  c = LightingLambert (o, IN.lightDir, atten);
  #endif // LIGHTMAP_OFF
  #ifdef LIGHTMAP_OFF
  c.rgb += o.Albedo * IN.vlight;
  #endif // LIGHTMAP_OFF
  #ifndef LIGHTMAP_OFF
  #ifdef DIRLIGHTMAP_OFF
  fixed4 lmtex = tex2D(unity_Lightmap, IN.lmap.xy);
  fixed3 lm = DecodeLightmap (lmtex);
  #else
  fixed4 lmtex = tex2D(unity_Lightmap, IN.lmap.xy);
  fixed4 lmIndTex = tex2D(unity_LightmapInd, IN.lmap.xy);
  half3 lm = LightingLambert_DirLightmap(o, lmtex, lmIndTex, 1).rgb;
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
  return LLEncodeGamma( c );
}

ENDCG
}
Pass {
		Name "FORWARD"
		Tags { "LightMode" = "ForwardAdd" }
		ZWrite Off Blend One One Fog { Color (0,0,0,0) }
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
#line 12

#pragma debug
//#pragma surface surf Lambert

sampler2D _MainTex;
sampler2D _BumpMap;
fixed4 _Color;

struct Input {
	float2 uv_MainTex;
	float2 uv_BumpMap;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = LLDecodeTex( tex2D(_MainTex, IN.uv_MainTex) ) * LLDecodeGamma( _Color );
	o.Albedo = c.rgb;
	o.Alpha = c.a;
	o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
}
struct v2f_surf {
  float4 pos : SV_POSITION;
  float4 pack0 : TEXCOORD0;
  half3 lightDir : TEXCOORD1;
  LIGHTING_COORDS(2,3)
};
float4 _MainTex_ST;
float4 _BumpMap_ST;
v2f_surf vert_surf (appdata_full v) {
  v2f_surf o;
  o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
  o.pack0.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
  o.pack0.zw = TRANSFORM_TEX(v.texcoord, _BumpMap);
  TANGENT_SPACE_ROTATION;
  float3 lightDir = mul (rotation, ObjSpaceLightDir(v.vertex));
  o.lightDir = lightDir;
  TRANSFER_VERTEX_TO_FRAGMENT(o);
  return o;
}
fixed4 frag_surf (v2f_surf IN) : COLOR {
  Input surfIN;
  surfIN.uv_MainTex = IN.pack0.xy;
  surfIN.uv_BumpMap = IN.pack0.zw;
  SurfaceOutput o;
  o.Albedo = 0.0;
  o.Emission = 0.0;
  o.Specular = 0.0;
  o.Alpha = 0.0;
  o.Gloss = 0.0;
  surf (surfIN, o);
  #ifndef USING_DIRECTIONAL_LIGHT
  fixed3 lightDir = normalize(IN.lightDir);
  #else
  fixed3 lightDir = IN.lightDir;
  #endif
  fixed4 c = LightingLambert (o, lightDir, LIGHT_ATTENUATION(IN));
  c.a = 0.0;
  return LLEncodeGamma( c );
}

ENDCG
}
Pass {
		Name "PREPASS"
		Tags { "LightMode" = "PrePassBase" }
		Fog {Mode Off}
CGPROGRAM
#pragma target 3.0
#include "LinLighting.cginc"
#pragma vertex vert_surf
#pragma fragment frag_surf
#pragma fragmentoption ARB_precision_hint_fastest

#include "HLSLSupport.cginc"
#define UNITY_PASS_PREPASSBASE
#include "UnityCGGoLin.cginc"
#include "Lighting.cginc"

#define INTERNAL_DATA
#define WorldReflectionVector(data,normal) data.worldRefl
#define WorldNormalVector(data,normal) normal
#line 1
#line 12

#pragma debug
//#pragma surface surf Lambert

sampler2D _MainTex;
sampler2D _BumpMap;
fixed4 _Color;

struct Input {
	float2 uv_MainTex;
	float2 uv_BumpMap;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = LLDecodeTex( tex2D(_MainTex, IN.uv_MainTex) ) * LLDecodeGamma( _Color );
	o.Albedo = c.rgb;
	o.Alpha = c.a;
	o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
}
struct v2f_surf {
  float4 pos : SV_POSITION;
  float2 pack0 : TEXCOORD0;
  float3 TtoW0 : TEXCOORD1;
  float3 TtoW1 : TEXCOORD2;
  float3 TtoW2 : TEXCOORD3;
};
float4 _BumpMap_ST;
v2f_surf vert_surf (appdata_full v) {
  v2f_surf o;
  o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
  o.pack0.xy = TRANSFORM_TEX(v.texcoord, _BumpMap);
  TANGENT_SPACE_ROTATION;
  o.TtoW0 = mul(rotation, ((float3x3)_Object2World)[0].xyz)*unity_Scale.w;
  o.TtoW1 = mul(rotation, ((float3x3)_Object2World)[1].xyz)*unity_Scale.w;
  o.TtoW2 = mul(rotation, ((float3x3)_Object2World)[2].xyz)*unity_Scale.w;
  return o;
}
fixed4 frag_surf (v2f_surf IN) : COLOR {
  Input surfIN;
  surfIN.uv_BumpMap = IN.pack0.xy;
  SurfaceOutput o;
  o.Albedo = 0.0;
  o.Emission = 0.0;
  o.Specular = 0.0;
  o.Alpha = 0.0;
  o.Gloss = 0.0;
  surf (surfIN, o);
  fixed3 worldN;
  worldN.x = dot(IN.TtoW0, o.Normal);
  worldN.y = dot(IN.TtoW1, o.Normal);
  worldN.z = dot(IN.TtoW2, o.Normal);
  o.Normal = worldN;
  fixed4 res;
  res.rgb = o.Normal * 0.5 + 0.5;
  res.a = o.Specular;
  return LLEncodeGamma( res );
}

ENDCG
}
Pass {
		Name "PREPASS"
		Tags { "LightMode" = "PrePassFinal" }
		ZWrite Off
CGPROGRAM
#pragma target 3.0
#include "LinLighting.cginc"
#pragma vertex vert_surf
#pragma fragment frag_surf
#pragma fragmentoption ARB_precision_hint_fastest
#pragma multi_compile_prepassfinal
#include "HLSLSupport.cginc"
#define UNITY_PASS_PREPASSFINAL
#include "UnityCGGoLin.cginc"
#include "Lighting.cginc"

#define INTERNAL_DATA
#define WorldReflectionVector(data,normal) data.worldRefl
#define WorldNormalVector(data,normal) normal
#line 1
#line 12

#pragma debug
//#pragma surface surf Lambert

sampler2D _MainTex;
sampler2D _BumpMap;
fixed4 _Color;

struct Input {
	float2 uv_MainTex;
	float2 uv_BumpMap;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = LLDecodeTex( tex2D(_MainTex, IN.uv_MainTex) ) * LLDecodeGamma( _Color );
	o.Albedo = c.rgb;
	o.Alpha = c.a;
	o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
}
struct v2f_surf {
  float4 pos : SV_POSITION;
  float4 pack0 : TEXCOORD0;
  float4 screen : TEXCOORD1;
#ifdef LIGHTMAP_OFF
  float3 vlight : TEXCOORD2;
#else
  float2 lmap : TEXCOORD2;
#ifdef DIRLIGHTMAP_OFF
  float4 lmapFadePos : TEXCOORD3;
#endif
#endif
};
#ifndef LIGHTMAP_OFF
float4 unity_LightmapST;
#ifdef DIRLIGHTMAP_OFF
// float4 unity_ShadowFadeCenterAndType;
#endif
#endif
float4 _MainTex_ST;
float4 _BumpMap_ST;
v2f_surf vert_surf (appdata_full v) {
  v2f_surf o;
  o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
  o.pack0.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
  o.pack0.zw = TRANSFORM_TEX(v.texcoord, _BumpMap);
  o.screen = ComputeScreenPos (o.pos);
#ifndef LIGHTMAP_OFF
  o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
  #ifdef DIRLIGHTMAP_OFF
    o.lmapFadePos.xyz = (mul(_Object2World, v.vertex).xyz - unity_ShadowFadeCenterAndType.xyz) * unity_ShadowFadeCenterAndType.w;
    o.lmapFadePos.w = (-mul(UNITY_MATRIX_MV, v.vertex).z) * (1.0 - unity_ShadowFadeCenterAndType.w);
  #endif
#else
  float3 worldN = mul((float3x3)_Object2World, SCALED_NORMAL);
  o.vlight = ShadeSH9 (float4(worldN,1.0));
#endif
  return o;
}
sampler2D _LightBuffer;
#if defined (SHADER_API_XBOX360) && defined (HDR_LIGHT_PREPASS_ON)
sampler2D _LightSpecBuffer;
#endif
#ifndef LIGHTMAP_OFF
sampler2D unity_Lightmap;
sampler2D unity_LightmapInd;
float4 unity_LightmapFade;
#endif
fixed4 unity_Ambient;
fixed4 frag_surf (v2f_surf IN) : COLOR {
  Input surfIN;
  surfIN.uv_MainTex = IN.pack0.xy;
  surfIN.uv_BumpMap = IN.pack0.zw;
  SurfaceOutput o;
  o.Albedo = 0.0;
  o.Emission = 0.0;
  o.Specular = 0.0;
  o.Alpha = 0.0;
  o.Gloss = 0.0;
  surf (surfIN, o);
  half4 light = tex2Dproj (_LightBuffer, UNITY_PROJ_COORD(IN.screen));
#if defined (SHADER_API_GLES)
  light = max(light, half4(0.001));
#endif
#ifndef HDR_LIGHT_PREPASS_ON
  light = -log2(light);
#endif
#if defined (SHADER_API_XBOX360) && defined (HDR_LIGHT_PREPASS_ON)
  light.w = tex2Dproj (_LightSpecBuffer, UNITY_PROJ_COORD(IN.screen)).r;
#endif
#ifndef LIGHTMAP_OFF
#ifdef DIRLIGHTMAP_OFF
  half3 lmFull = LLDecodeGamma( DecodeLightmap (tex2D(unity_Lightmap, IN.lmap.xy)) );
  half3 lmIndirect = LLDecodeGamma( DecodeLightmap (tex2D(unity_LightmapInd, IN.lmap.xy)) );
  float lmFade = length (IN.lmapFadePos) * unity_LightmapFade.z + unity_LightmapFade.w;
  half3 lm = lerp (lmIndirect, lmFull, saturate(lmFade));
  light.rgb += lm;
#else
  fixed4 lmtex = tex2D(unity_Lightmap, IN.lmap.xy);
  fixed4 lmIndTex = tex2D(unity_LightmapInd, IN.lmap.xy);
  half4 lm = LightingLambert_DirLightmap(o, lmtex, lmIndTex, 1);
  light += lm;
#endif
#else
  light.rgb += IN.vlight;
#endif
  half4 c = LightingLambert_PrePass (o, light);
  return LLEncodeGamma( c );
}

ENDCG
}
}
Fallback "Linear Lighting/Diffuse"
}
