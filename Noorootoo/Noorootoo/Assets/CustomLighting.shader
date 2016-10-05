// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:0,x:34330,y:31982,varname:node_0,prsc:2|custl-7489-OUT,olwid-8659-OUT,olcol-6197-RGB;n:type:ShaderForge.SFN_LightAttenuation,id:37,x:33872,y:32026,varname:node_37,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:41,x:32417,y:32296,prsc:2,pt:True;n:type:ShaderForge.SFN_LightVector,id:42,x:32417,y:32150,varname:node_42,prsc:2;n:type:ShaderForge.SFN_Add,id:55,x:33872,y:32270,varname:node_55,prsc:2|A-84-OUT,B-187-RGB;n:type:ShaderForge.SFN_LightColor,id:63,x:33872,y:32155,varname:node_63,prsc:2;n:type:ShaderForge.SFN_Multiply,id:64,x:34056,y:32155,varname:node_64,prsc:2|A-63-RGB,B-55-OUT,C-37-OUT;n:type:ShaderForge.SFN_Color,id:80,x:33368,y:32178,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:82,x:33368,y:32002,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:_Diffuse,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b66bceaf0cc0ace4e9bdc92f14bba709,ntxv:0,isnm:False|UVIN-272-UVOUT;n:type:ShaderForge.SFN_Multiply,id:84,x:33573,y:32160,cmnt:Diffuse Light,varname:node_84,prsc:2|A-82-RGB,B-80-RGB,C-6322-OUT;n:type:ShaderForge.SFN_AmbientLight,id:187,x:33573,y:32280,varname:node_187,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:216,x:33091,y:32169,ptovrint:False,ptlb:Bands,ptin:_Bands,varname:_Bands,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Vector1,id:255,x:34537,y:32387,varname:node_255,prsc:2,v1:0.0051;n:type:ShaderForge.SFN_Posterize,id:264,x:33091,y:32222,varname:node_264,prsc:2|IN-3385-OUT,STPS-216-OUT;n:type:ShaderForge.SFN_TexCoord,id:272,x:33368,y:31835,varname:node_272,prsc:2,uv:0;n:type:ShaderForge.SFN_Color,id:6197,x:34726,y:32570,ptovrint:False,ptlb:Outline Colour,ptin:_OutlineColour,varname:node_6197,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Slider,id:3349,x:34405,y:32464,ptovrint:False,ptlb:Outline Width,ptin:_OutlineWidth,varname:node_3349,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:10,max:10;n:type:ShaderForge.SFN_Multiply,id:8659,x:34726,y:32405,varname:node_8659,prsc:2|A-255-OUT,B-3349-OUT;n:type:ShaderForge.SFN_Multiply,id:8489,x:33872,y:32397,varname:node_8489,prsc:2|A-82-RGB,B-2691-OUT;n:type:ShaderForge.SFN_Multiply,id:7489,x:34056,y:32331,varname:node_7489,prsc:2|A-64-OUT,B-8489-OUT;n:type:ShaderForge.SFN_Dot,id:6770,x:32644,y:32150,varname:node_6770,prsc:2,dt:4|A-42-OUT,B-41-OUT;n:type:ShaderForge.SFN_Vector1,id:2691,x:33573,y:32422,varname:node_2691,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:5630,x:33056,y:32483,varname:node_5630,prsc:2,v1:1;n:type:ShaderForge.SFN_Slider,id:6258,x:32934,y:32389,ptovrint:False,ptlb:Shadow brightness,ptin:_Shadowbrightness,varname:node_6258,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:6322,x:33382,y:32323,varname:node_6322,prsc:2|IN-264-OUT,IMIN-8996-OUT,IMAX-5630-OUT,OMIN-6258-OUT,OMAX-5630-OUT;n:type:ShaderForge.SFN_Vector1,id:8996,x:33056,y:32544,varname:node_8996,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:3385,x:32852,y:32222,varname:node_3385,prsc:2|A-6770-OUT,B-7655-OUT;n:type:ShaderForge.SFN_Vector1,id:7655,x:32630,y:32395,varname:node_7655,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Clamp01,id:1226,x:33039,y:31932,varname:node_1226,prsc:2|IN-264-OUT;n:type:ShaderForge.SFN_Tex2d,id:6240,x:33139,y:32639,ptovrint:False,ptlb:node_6240,ptin:_node_6240,varname:node_6240,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:511956832000075459b5ec7ca2e77ad7,ntxv:0,isnm:False|UVIN-9984-OUT;n:type:ShaderForge.SFN_Append,id:9984,x:32807,y:32483,varname:node_9984,prsc:2|A-6770-OUT,B-5181-OUT;n:type:ShaderForge.SFN_Vector1,id:5181,x:32506,y:32578,varname:node_5181,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:2449,x:32608,y:31978,varname:node_2449,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4720,x:33425,y:32697,varname:node_4720,prsc:2|A-264-OUT,B-6240-RGB;proporder:80-82-216-6197-3349-6258-6240;pass:END;sub:END;*/

Shader "Shader Forge/Examples/Custom Lighting" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Bands ("Bands", Float ) = 3
        _OutlineColour ("Outline Colour", Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width", Range(0, 10)) = 10
        _Shadowbrightness ("Shadow brightness", Range(0, 1)) = 0
        _node_6240 ("node_6240", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 3.0
            uniform float4 _OutlineColour;
            uniform float _OutlineWidth;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = mul(UNITY_MATRIX_MVP, float4(v.vertex.xyz + v.normal*(0.0051*_OutlineWidth),1) );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                return fixed4(_OutlineColour.rgb,0);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _Bands;
            uniform float _Shadowbrightness;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float node_6770 = 0.5*dot(lightDirection,normalDirection)+0.5;
                float node_264 = floor((node_6770*1.5) * _Bands) / (_Bands - 1);
                float node_8996 = 0.0;
                float node_5630 = 1.0;
                float3 finalColor = ((_LightColor0.rgb*((_Diffuse_var.rgb*_Color.rgb*(_Shadowbrightness + ( (node_264 - node_8996) * (node_5630 - _Shadowbrightness) ) / (node_5630 - node_8996)))+UNITY_LIGHTMODEL_AMBIENT.rgb)*attenuation)*(_Diffuse_var.rgb*1.0));
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
