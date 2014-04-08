// Specular, Normal Maps with Main Texture for highlight
// Fragment based
// Date: 2013/10/30
// Author: zzc
Shader "Mobile/Tang/Transparent/UV_RGBA_Scale"
{
  Properties 
  {
    _MainTex ("Texture", 2D) = "white" {}
    _tangUV("UV Offset u1 v1", Vector )		= ( 0, 0, 0, 0 )
    _tangRGBA ( "RGB Multiply", Vector )		= ( 1, 1, 1, 1 )    
    _tangScale ("Scale XYZ", Vector )		= ( 1.0, 1.0, 1.0, 1.0 )
  }

  //=========================================================================
  SubShader 
  {
    Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }

    Pass 
    {    
      ZWrite Off
      Blend SrcAlpha OneMinusSrcAlpha
      Cull Back

      Program "vp" {
// Vertex combos: 1
//   opengl - ALU: 9 to 9
//   d3d9 - ALU: 18 to 18
//   d3d11 - ALU: 11 to 11, TEX: 0 to 0, FLOW: 1 to 1
//   d3d11_9x - ALU: 11 to 11, TEX: 0 to 0, FLOW: 1 to 1
SubProgram "opengl " {
Keywords { }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "color" Color
Vector 5 [_MainTex_ST]
Vector 6 [_tangUV]
Vector 7 [_tangRGBA]
Vector 8 [_tangScale]
"!!ARBvp1.0
# 9 ALU
PARAM c[9] = { program.local[0],
		state.matrix.mvp,
		program.local[5..8] };
TEMP R0;
TEMP R1;
MUL R0, vertex.position, c[8];
DP4 result.position.w, R0, c[4];
DP4 result.position.z, R0, c[3];
DP4 result.position.y, R0, c[2];
ADD R1.xy, vertex.texcoord[0], c[6];
DP4 result.position.x, R0, c[1];
ADD R0.xy, R1, c[5].zwzw;
MUL result.texcoord[1], vertex.color, c[7];
MUL result.texcoord[0].xy, R0, c[5];
END
# 9 instructions, 2 R-regs
"
}

SubProgram "d3d9 " {
Keywords { }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "color" Color
Matrix 0 [glstate_matrix_mvp]
Vector 4 [_MainTex_ST]
Vector 5 [_MainTex_TexelSize]
Vector 6 [_tangUV]
Vector 7 [_tangRGBA]
Vector 8 [_tangScale]
"vs_2_0
; 18 ALU
def c9, 0.00000000, 1.00000000, 0, 0
dcl_position0 v0
dcl_texcoord0 v1
dcl_color0 v2
mul r0, v0, c8
dp4 oPos.w, r0, c3
dp4 oPos.z, r0, c2
dp4 oPos.y, r0, c1
dp4 oPos.x, r0, c0
mov r1.x, c9
slt r0.x, c5.y, r1
max r0.x, -r0, r0
slt r0.x, c9, r0
add r0.zw, v1.xyxy, c6.xyxy
add r0.zw, r0, c4
mul r0.zw, r0, c4.xyxy
add r0.y, -r0.x, c9
mul r1.x, r0.w, r0.y
add r0.y, -r0.w, c9
mul oT1, v2, c7
mad oT0.y, r0.x, r0, r1.x
mov oT0.x, r0.z
"
}

SubProgram "d3d11 " {
Keywords { }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "color" Color
ConstBuffer "$Globals" 96 // 96 used size, 6 vars
Vector 16 [_MainTex_ST] 4
Vector 32 [_MainTex_TexelSize] 4
Vector 48 [_tangUV] 4
Vector 64 [_tangRGBA] 4
Vector 80 [_tangScale] 4
ConstBuffer "UnityPerDraw" 336 // 64 used size, 6 vars
Matrix 0 [glstate_matrix_mvp] 4
BindCB "$Globals" 0
BindCB "UnityPerDraw" 1
// 14 instructions, 2 temp regs, 0 temp arrays:
// ALU 11 float, 0 int, 0 uint
// TEX 0 (0 load, 0 comp, 0 bias, 0 grad)
// FLOW 1 static, 0 dynamic
"vs_4_0
eefiecedoikbjmjhabegfbpfehigpaigafehffgkabaaaaaafaadaaaaadaaaaaa
cmaaaaaajmaaaaaaamabaaaaejfdeheogiaaaaaaadaaaaaaaiaaaaaafaaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaafjaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaagcaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
apapaaaafaepfdejfeejepeoaafeeffiedepepfceeaaedepemepfcaaepfdeheo
giaaaaaaadaaaaaaaiaaaaaafaaaaaaaaaaaaaaaabaaaaaaadaaaaaaaaaaaaaa
apaaaaaafmaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaafmaaaaaa
abaaaaaaaaaaaaaaadaaaaaaacaaaaaaapaaaaaafdfgfpfaepfdejfeejepeoaa
feeffiedepepfceeaaklklklfdeieefcdmacaaaaeaaaabaaipaaaaaafjaaaaae
egiocaaaaaaaaaaaagaaaaaafjaaaaaeegiocaaaabaaaaaaaeaaaaaafpaaaaad
pcbabaaaaaaaaaaafpaaaaaddcbabaaaabaaaaaafpaaaaadpcbabaaaacaaaaaa
ghaaaaaepccabaaaaaaaaaaaabaaaaaagfaaaaaddccabaaaabaaaaaagfaaaaad
pccabaaaacaaaaaagiaaaaacacaaaaaadiaaaaaipcaabaaaaaaaaaaaegbobaaa
aaaaaaaaegiocaaaaaaaaaaaafaaaaaadiaaaaaipcaabaaaabaaaaaafgafbaaa
aaaaaaaaegiocaaaabaaaaaaabaaaaaadcaaaaakpcaabaaaabaaaaaaegiocaaa
abaaaaaaaaaaaaaaagaabaaaaaaaaaaaegaobaaaabaaaaaadcaaaaakpcaabaaa
abaaaaaaegiocaaaabaaaaaaacaaaaaakgakbaaaaaaaaaaaegaobaaaabaaaaaa
dcaaaaakpccabaaaaaaaaaaaegiocaaaabaaaaaaadaaaaaapgapbaaaaaaaaaaa
egaobaaaabaaaaaadbaaaaaibcaabaaaaaaaaaaabkiacaaaaaaaaaaaacaaaaaa
abeaaaaaaaaaaaaaaaaaaaaigcaabaaaaaaaaaaaagbbbaaaabaaaaaaagibcaaa
aaaaaaaaadaaaaaaaaaaaaaigcaabaaaaaaaaaaafgagbaaaaaaaaaaakgilcaaa
aaaaaaaaabaaaaaadcaaaaalicaabaaaaaaaaaaabkiacaiaebaaaaaaaaaaaaaa
abaaaaaackaabaaaaaaaaaaaabeaaaaaaaaaiadpdiaaaaaidcaabaaaabaaaaaa
jgafbaaaaaaaaaaaegiacaaaaaaaaaaaabaaaaaadhaaaaajecaabaaaabaaaaaa
akaabaaaaaaaaaaadkaabaaaaaaaaaaabkaabaaaabaaaaaadgaaaaafdccabaaa
abaaaaaaigaabaaaabaaaaaadiaaaaaipccabaaaacaaaaaaegbobaaaacaaaaaa
egiocaaaaaaaaaaaaeaaaaaadoaaaaab"
}

SubProgram "gles " {
Keywords { }
"!!GLES


#ifdef VERTEX

varying lowp vec4 xlv_TEXCOORD1;
varying mediump vec2 xlv_TEXCOORD0;
uniform highp vec4 _tangScale;
uniform highp vec4 _tangRGBA;
uniform highp vec4 _tangUV;
uniform highp vec4 _MainTex_ST;
uniform highp mat4 glstate_matrix_mvp;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesColor;
attribute vec4 _glesVertex;
void main ()
{
  mediump vec2 tmpvar_1;
  lowp vec4 tmpvar_2;
  highp vec4 tmpvar_3;
  tmpvar_3 = (_glesColor * _tangRGBA);
  tmpvar_2 = tmpvar_3;
  highp vec2 tmpvar_4;
  tmpvar_4 = (_MainTex_ST.xy * ((_glesMultiTexCoord0.xy + _tangUV.xy) + _MainTex_ST.zw));
  tmpvar_1 = tmpvar_4;
  gl_Position = (glstate_matrix_mvp * (_glesVertex * _tangScale));
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT

varying lowp vec4 xlv_TEXCOORD1;
varying mediump vec2 xlv_TEXCOORD0;
uniform sampler2D _MainTex;
void main ()
{
  gl_FragData[0] = (texture2D (_MainTex, xlv_TEXCOORD0) * xlv_TEXCOORD1);
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { }
"!!GLES


#ifdef VERTEX

varying lowp vec4 xlv_TEXCOORD1;
varying mediump vec2 xlv_TEXCOORD0;
uniform highp vec4 _tangScale;
uniform highp vec4 _tangRGBA;
uniform highp vec4 _tangUV;
uniform highp vec4 _MainTex_ST;
uniform highp mat4 glstate_matrix_mvp;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesColor;
attribute vec4 _glesVertex;
void main ()
{
  mediump vec2 tmpvar_1;
  lowp vec4 tmpvar_2;
  highp vec4 tmpvar_3;
  tmpvar_3 = (_glesColor * _tangRGBA);
  tmpvar_2 = tmpvar_3;
  highp vec2 tmpvar_4;
  tmpvar_4 = (_MainTex_ST.xy * ((_glesMultiTexCoord0.xy + _tangUV.xy) + _MainTex_ST.zw));
  tmpvar_1 = tmpvar_4;
  gl_Position = (glstate_matrix_mvp * (_glesVertex * _tangScale));
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT

varying lowp vec4 xlv_TEXCOORD1;
varying mediump vec2 xlv_TEXCOORD0;
uniform sampler2D _MainTex;
void main ()
{
  gl_FragData[0] = (texture2D (_MainTex, xlv_TEXCOORD0) * xlv_TEXCOORD1);
}



#endif"
}

SubProgram "flash " {
Keywords { }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "color" Color
Matrix 0 [glstate_matrix_mvp]
Vector 4 [_MainTex_ST]
Vector 5 [_MainTex_TexelSize]
Vector 6 [_tangUV]
Vector 7 [_tangRGBA]
Vector 8 [_tangScale]
"agal_vs
c9 0.0 1.0 0.0 0.0
[bc]
adaaaaaaaaaaapacaaaaaaoeaaaaaaaaaiaaaaoeabaaaaaa mul r0, a0, c8
bdaaaaaaaaaaaiadaaaaaaoeacaaaaaaadaaaaoeabaaaaaa dp4 o0.w, r0, c3
bdaaaaaaaaaaaeadaaaaaaoeacaaaaaaacaaaaoeabaaaaaa dp4 o0.z, r0, c2
bdaaaaaaaaaaacadaaaaaaoeacaaaaaaabaaaaoeabaaaaaa dp4 o0.y, r0, c1
bdaaaaaaaaaaabadaaaaaaoeacaaaaaaaaaaaaoeabaaaaaa dp4 o0.x, r0, c0
aaaaaaaaabaaabacajaaaaoeabaaaaaaaaaaaaaaaaaaaaaa mov r1.x, c9
ckaaaaaaaaaaabacafaaaaffabaaaaaaabaaaaaaacaaaaaa slt r0.x, c5.y, r1.x
bfaaaaaaabaaacacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa neg r1.y, r0.x
ahaaaaaaaaaaabacabaaaaffacaaaaaaaaaaaaaaacaaaaaa max r0.x, r1.y, r0.x
ckaaaaaaaaaaabacajaaaaoeabaaaaaaaaaaaaaaacaaaaaa slt r0.x, c9, r0.x
abaaaaaaaaaaamacadaaaaeeaaaaaaaaagaaaaeeabaaaaaa add r0.zw, a3.xyxy, c6.xyxy
abaaaaaaaaaaamacaaaaaaopacaaaaaaaeaaaaoeabaaaaaa add r0.zw, r0.wwzw, c4
adaaaaaaaaaaamacaaaaaaopacaaaaaaaeaaaaeeabaaaaaa mul r0.zw, r0.wwzw, c4.xyxy
bfaaaaaaacaaabacaaaaaaaaacaaaaaaaaaaaaaaaaaaaaaa neg r2.x, r0.x
abaaaaaaaaaaacacacaaaaaaacaaaaaaajaaaaoeabaaaaaa add r0.y, r2.x, c9
adaaaaaaabaaabacaaaaaappacaaaaaaaaaaaaffacaaaaaa mul r1.x, r0.w, r0.y
bfaaaaaaacaaaiacaaaaaappacaaaaaaaaaaaaaaaaaaaaaa neg r2.w, r0.w
abaaaaaaaaaaacacacaaaappacaaaaaaajaaaaoeabaaaaaa add r0.y, r2.w, c9
adaaaaaaabaaapaeacaaaaoeaaaaaaaaahaaaaoeabaaaaaa mul v1, a2, c7
adaaaaaaacaaacacaaaaaaaaacaaaaaaaaaaaaffacaaaaaa mul r2.y, r0.x, r0.y
abaaaaaaaaaaacaeacaaaaffacaaaaaaabaaaaaaacaaaaaa add v0.y, r2.y, r1.x
aaaaaaaaaaaaabaeaaaaaakkacaaaaaaaaaaaaaaaaaaaaaa mov v0.x, r0.z
aaaaaaaaaaaaamaeaaaaaaoeabaaaaaaaaaaaaaaaaaaaaaa mov v0.zw, c0
"
}

SubProgram "d3d11_9x " {
Keywords { }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "color" Color
ConstBuffer "$Globals" 96 // 96 used size, 6 vars
Vector 16 [_MainTex_ST] 4
Vector 32 [_MainTex_TexelSize] 4
Vector 48 [_tangUV] 4
Vector 64 [_tangRGBA] 4
Vector 80 [_tangScale] 4
ConstBuffer "UnityPerDraw" 336 // 64 used size, 6 vars
Matrix 0 [glstate_matrix_mvp] 4
BindCB "$Globals" 0
BindCB "UnityPerDraw" 1
// 14 instructions, 2 temp regs, 0 temp arrays:
// ALU 11 float, 0 int, 0 uint
// TEX 0 (0 load, 0 comp, 0 bias, 0 grad)
// FLOW 1 static, 0 dynamic
"vs_4_0_level_9_1
eefiecedbnnedingmmjjopomgcoebgjnhblpbikmabaaaaaaomaeaaaaaeaaaaaa
daaaaaaamiabaaaaamaeaaaahmaeaaaaebgpgodjjaabaaaajaabaaaaaaacpopp
faabaaaaeaaaaaaaacaaceaaaaaadmaaaaaadmaaaaaaceaaabaadmaaaaaaabaa
afaaabaaaaaaaaaaabaaaaaaaeaaagaaaaaaaaaaaaaaaaaaaaacpoppfbaaaaaf
akaaapkaaaaaaaaaaaaaaamaaaaaiadpaaaaaaaabpaaaaacafaaaaiaaaaaapja
bpaaaaacafaaabiaabaaapjabpaaaaacafaaaciaacaaapjaafaaaaadabaaapoa
acaaoejaaeaaoekaafaaaaadaaaaapiaaaaaoejaafaaoekaafaaaaadabaaapia
aaaaffiaahaaoekaaeaaaaaeabaaapiaagaaoekaaaaaaaiaabaaoeiaaeaaaaae
abaaapiaaiaaoekaaaaakkiaabaaoeiaaeaaaaaeaaaaapiaajaaoekaaaaappia
abaaoeiaaeaaaaaeaaaaadmaaaaappiaaaaaoekaaaaaoeiaabaaaaacaaaaamma
aaaaoeiaabaaaaacaaaaabiaakaaaakaamaaaaadaaaaabiaacaaffkaaaaaaaia
acaaaaadaaaaagiaabaanajaadaanakaacaaaaadaaaaagiaaaaaoeiaabaapika
afaaaaadabaaadiaaaaaojiaabaaoekaaeaaaaaeaaaaaciaabaaffiaakaaffka
akaakkkaaeaaaaaeabaaaeiaaaaaaaiaaaaaffiaabaaffiaabaaaaacaaaaadoa
abaaoiiappppaaaafdeieefcdmacaaaaeaaaabaaipaaaaaafjaaaaaeegiocaaa
aaaaaaaaagaaaaaafjaaaaaeegiocaaaabaaaaaaaeaaaaaafpaaaaadpcbabaaa
aaaaaaaafpaaaaaddcbabaaaabaaaaaafpaaaaadpcbabaaaacaaaaaaghaaaaae
pccabaaaaaaaaaaaabaaaaaagfaaaaaddccabaaaabaaaaaagfaaaaadpccabaaa
acaaaaaagiaaaaacacaaaaaadiaaaaaipcaabaaaaaaaaaaaegbobaaaaaaaaaaa
egiocaaaaaaaaaaaafaaaaaadiaaaaaipcaabaaaabaaaaaafgafbaaaaaaaaaaa
egiocaaaabaaaaaaabaaaaaadcaaaaakpcaabaaaabaaaaaaegiocaaaabaaaaaa
aaaaaaaaagaabaaaaaaaaaaaegaobaaaabaaaaaadcaaaaakpcaabaaaabaaaaaa
egiocaaaabaaaaaaacaaaaaakgakbaaaaaaaaaaaegaobaaaabaaaaaadcaaaaak
pccabaaaaaaaaaaaegiocaaaabaaaaaaadaaaaaapgapbaaaaaaaaaaaegaobaaa
abaaaaaadbaaaaaibcaabaaaaaaaaaaabkiacaaaaaaaaaaaacaaaaaaabeaaaaa
aaaaaaaaaaaaaaaigcaabaaaaaaaaaaaagbbbaaaabaaaaaaagibcaaaaaaaaaaa
adaaaaaaaaaaaaaigcaabaaaaaaaaaaafgagbaaaaaaaaaaakgilcaaaaaaaaaaa
abaaaaaadcaaaaalicaabaaaaaaaaaaabkiacaiaebaaaaaaaaaaaaaaabaaaaaa
ckaabaaaaaaaaaaaabeaaaaaaaaaiadpdiaaaaaidcaabaaaabaaaaaajgafbaaa
aaaaaaaaegiacaaaaaaaaaaaabaaaaaadhaaaaajecaabaaaabaaaaaaakaabaaa
aaaaaaaadkaabaaaaaaaaaaabkaabaaaabaaaaaadgaaaaafdccabaaaabaaaaaa
igaabaaaabaaaaaadiaaaaaipccabaaaacaaaaaaegbobaaaacaaaaaaegiocaaa
aaaaaaaaaeaaaaaadoaaaaabejfdeheogiaaaaaaadaaaaaaaiaaaaaafaaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaafjaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaagcaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
apapaaaafaepfdejfeejepeoaafeeffiedepepfceeaaedepemepfcaaepfdeheo
giaaaaaaadaaaaaaaiaaaaaafaaaaaaaaaaaaaaaabaaaaaaadaaaaaaaaaaaaaa
apaaaaaafmaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaafmaaaaaa
abaaaaaaaaaaaaaaadaaaaaaacaaaaaaapaaaaaafdfgfpfaepfdejfeejepeoaa
feeffiedepepfceeaaklklkl"
}

SubProgram "gles3 " {
Keywords { }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;

#line 151
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 187
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 181
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 328
struct Varys {
    highp vec4 pos;
    mediump vec2 tc1;
    lowp vec4 vcolor;
};
#line 321
struct VertInput {
    highp vec4 vertex;
    highp vec2 texcoord;
    highp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[8];
uniform highp vec4 unity_LightPosition[8];
uniform highp vec4 unity_LightAtten[8];
#line 19
uniform highp vec4 unity_SpotDirection[8];
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
#line 23
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
#line 27
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
#line 31
uniform highp vec4 _LightSplitsNear;
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
#line 35
uniform highp vec4 unity_ShadowFadeCenterAndType;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
#line 39
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
#line 43
uniform highp mat4 glstate_matrix_texture0;
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
#line 47
uniform highp mat4 glstate_matrix_projection;
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
#line 51
uniform lowp vec4 unity_ColorSpaceGrey;
#line 77
#line 82
#line 87
#line 91
#line 96
#line 120
#line 137
#line 158
#line 166
#line 193
#line 206
#line 215
#line 220
#line 229
#line 234
#line 243
#line 260
#line 265
#line 291
#line 299
#line 307
#line 311
#line 315
uniform sampler2D _MainTex;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _MainTex_TexelSize;
uniform highp vec4 _tangUV;
#line 319
uniform highp vec4 _tangRGBA;
uniform highp vec4 _tangScale;
#line 335
#line 343
#line 335
Varys vert( in VertInput ad ) {
    Varys v;
    v.vcolor = (ad.color * _tangRGBA);
    #line 339
    v.pos = (glstate_matrix_mvp * (ad.vertex * _tangScale));
    v.tc1 = (_MainTex_ST.xy * ((ad.texcoord.xy + _tangUV.xy) + _MainTex_ST.zw));
    return v;
}
out mediump vec2 xlv_TEXCOORD0;
out lowp vec4 xlv_TEXCOORD1;
void main() {
    Varys xl_retval;
    VertInput xlt_ad;
    xlt_ad.vertex = vec4(gl_Vertex);
    xlt_ad.texcoord = vec2(gl_MultiTexCoord0);
    xlt_ad.color = vec4(gl_Color);
    xl_retval = vert( xlt_ad);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec2(xl_retval.tc1);
    xlv_TEXCOORD1 = vec4(xl_retval.vcolor);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];

#line 151
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 187
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 181
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 328
struct Varys {
    highp vec4 pos;
    mediump vec2 tc1;
    lowp vec4 vcolor;
};
#line 321
struct VertInput {
    highp vec4 vertex;
    highp vec2 texcoord;
    highp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[8];
uniform highp vec4 unity_LightPosition[8];
uniform highp vec4 unity_LightAtten[8];
#line 19
uniform highp vec4 unity_SpotDirection[8];
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
#line 23
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
#line 27
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
#line 31
uniform highp vec4 _LightSplitsNear;
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
#line 35
uniform highp vec4 unity_ShadowFadeCenterAndType;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
#line 39
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
#line 43
uniform highp mat4 glstate_matrix_texture0;
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
#line 47
uniform highp mat4 glstate_matrix_projection;
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
#line 51
uniform lowp vec4 unity_ColorSpaceGrey;
#line 77
#line 82
#line 87
#line 91
#line 96
#line 120
#line 137
#line 158
#line 166
#line 193
#line 206
#line 215
#line 220
#line 229
#line 234
#line 243
#line 260
#line 265
#line 291
#line 299
#line 307
#line 311
#line 315
uniform sampler2D _MainTex;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _MainTex_TexelSize;
uniform highp vec4 _tangUV;
#line 319
uniform highp vec4 _tangRGBA;
uniform highp vec4 _tangScale;
#line 335
#line 343
#line 343
lowp vec4 frag( in Varys v ) {
    return (texture( _MainTex, v.tc1) * v.vcolor);
}
in mediump vec2 xlv_TEXCOORD0;
in lowp vec4 xlv_TEXCOORD1;
void main() {
    lowp vec4 xl_retval;
    Varys xlt_v;
    xlt_v.pos = vec4(0.0);
    xlt_v.tc1 = vec2(xlv_TEXCOORD0);
    xlt_v.vcolor = vec4(xlv_TEXCOORD1);
    xl_retval = frag( xlt_v);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}

}
Program "fp" {
// Fragment combos: 1
//   opengl - ALU: 2 to 2, TEX: 1 to 1
//   d3d9 - ALU: 2 to 2, TEX: 1 to 1
//   d3d11 - ALU: 1 to 1, TEX: 1 to 1, FLOW: 1 to 1
//   d3d11_9x - ALU: 1 to 1, TEX: 1 to 1, FLOW: 1 to 1
SubProgram "opengl " {
Keywords { }
SetTexture 0 [_MainTex] 2D
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 2 ALU, 1 TEX
TEMP R0;
TEX R0, fragment.texcoord[0], texture[0], 2D;
MUL result.color, R0, fragment.texcoord[1];
END
# 2 instructions, 1 R-regs
"
}

SubProgram "d3d9 " {
Keywords { }
SetTexture 0 [_MainTex] 2D
"ps_2_0
; 2 ALU, 1 TEX
dcl_2d s0
dcl t0.xy
dcl t1
texld r0, t0, s0
mul r0, r0, t1
mov_pp oC0, r0
"
}

SubProgram "d3d11 " {
Keywords { }
SetTexture 0 [_MainTex] 2D 0
// 3 instructions, 1 temp regs, 0 temp arrays:
// ALU 1 float, 0 int, 0 uint
// TEX 1 (0 load, 0 comp, 0 bias, 0 grad)
// FLOW 1 static, 0 dynamic
"ps_4_0
eefiecedjhmegfpchcmhidddkpfpjaohcifccijlabaaaaaagmabaaaaadaaaaaa
cmaaaaaajmaaaaaanaaaaaaaejfdeheogiaaaaaaadaaaaaaaiaaaaaafaaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaafmaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafmaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaa
apapaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklklepfdeheo
cmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaa
apaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcjeaaaaaaeaaaaaaacfaaaaaa
fkaaaaadaagabaaaaaaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaagcbaaaad
dcbabaaaabaaaaaagcbaaaadpcbabaaaacaaaaaagfaaaaadpccabaaaaaaaaaaa
giaaaaacabaaaaaaefaaaaajpcaabaaaaaaaaaaaegbabaaaabaaaaaaeghobaaa
aaaaaaaaaagabaaaaaaaaaaadiaaaaahpccabaaaaaaaaaaaegaobaaaaaaaaaaa
egbobaaaacaaaaaadoaaaaab"
}

SubProgram "gles " {
Keywords { }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { }
"!!GLES"
}

SubProgram "flash " {
Keywords { }
SetTexture 0 [_MainTex] 2D
"agal_ps
[bc]
ciaaaaaaaaaaapacaaaaaaoeaeaaaaaaaaaaaaaaafaababb tex r0, v0, s0 <2d wrap linear point>
adaaaaaaaaaaapacaaaaaaoeacaaaaaaabaaaaoeaeaaaaaa mul r0, r0, v1
aaaaaaaaaaaaapadaaaaaaoeacaaaaaaaaaaaaaaaaaaaaaa mov o0, r0
"
}

SubProgram "d3d11_9x " {
Keywords { }
SetTexture 0 [_MainTex] 2D 0
// 3 instructions, 1 temp regs, 0 temp arrays:
// ALU 1 float, 0 int, 0 uint
// TEX 1 (0 load, 0 comp, 0 bias, 0 grad)
// FLOW 1 static, 0 dynamic
"ps_4_0_level_9_1
eefiecedigaolanpfickabnopalkkfmncmmhioeeabaaaaaapiabaaaaaeaaaaaa
daaaaaaaliaaaaaafeabaaaameabaaaaebgpgodjiaaaaaaaiaaaaaaaaaacpppp
fiaaaaaaciaaaaaaaaaaciaaaaaaciaaaaaaciaaabaaceaaaaaaciaaaaaaaaaa
aaacppppbpaaaaacaaaaaaiaaaaacdlabpaaaaacaaaaaaiaabaaaplabpaaaaac
aaaaaajaaaaiapkaecaaaaadaaaaapiaaaaaoelaaaaioekaafaaaaadaaaacpia
aaaaoeiaabaaoelaabaaaaacaaaicpiaaaaaoeiappppaaaafdeieefcjeaaaaaa
eaaaaaaacfaaaaaafkaaaaadaagabaaaaaaaaaaafibiaaaeaahabaaaaaaaaaaa
ffffaaaagcbaaaaddcbabaaaabaaaaaagcbaaaadpcbabaaaacaaaaaagfaaaaad
pccabaaaaaaaaaaagiaaaaacabaaaaaaefaaaaajpcaabaaaaaaaaaaaegbabaaa
abaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaadiaaaaahpccabaaaaaaaaaaa
egaobaaaaaaaaaaaegbobaaaacaaaaaadoaaaaabejfdeheogiaaaaaaadaaaaaa
aiaaaaaafaaaaaaaaaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaafmaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadadaaaafmaaaaaaabaaaaaaaaaaaaaa
adaaaaaaacaaaaaaapapaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklkl"
}

SubProgram "gles3 " {
Keywords { }
"!!GLES3"
}

}

#LINE 77

      }
   } // eo Properties

} // eo Shader