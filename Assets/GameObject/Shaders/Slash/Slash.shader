// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32881,y:32667,varname:node_4795,prsc:2|emission-2462-OUT,alpha-798-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32235,y:32601,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a42dafdaba6683149872626ebd71b13b,ntxv:0,isnm:False|UVIN-5777-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-797-RGB,D-9248-OUT,E-1962-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.5019608,c3:1,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32235,y:33081,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:798,x:32495,y:32923,varname:node_798,prsc:2|A-6074-A,B-2053-A,C-797-A,D-1962-OUT;n:type:ShaderForge.SFN_TexCoord,id:4171,x:31176,y:32626,varname:node_4171,prsc:2,uv:0,uaff:True;n:type:ShaderForge.SFN_Panner,id:5777,x:32052,y:32601,varname:node_5777,prsc:2,spu:0,spv:1|UVIN-2022-OUT,DIST-76-OUT;n:type:ShaderForge.SFN_Multiply,id:4771,x:31386,y:32842,varname:node_4771,prsc:2|A-4171-V,B-5833-OUT;n:type:ShaderForge.SFN_Append,id:9965,x:31598,y:32567,varname:node_9965,prsc:2|A-4171-U,B-4771-OUT;n:type:ShaderForge.SFN_Multiply,id:2022,x:31842,y:32601,varname:node_2022,prsc:2|A-9965-OUT,B-4771-OUT;n:type:ShaderForge.SFN_Vector1,id:5833,x:31176,y:32842,varname:node_5833,prsc:2,v1:1;n:type:ShaderForge.SFN_OneMinus,id:2448,x:31690,y:33149,varname:node_2448,prsc:2|IN-4771-OUT;n:type:ShaderForge.SFN_Power,id:1129,x:31878,y:33149,varname:node_1129,prsc:2|VAL-2448-OUT,EXP-6293-OUT;n:type:ShaderForge.SFN_Multiply,id:4318,x:32061,y:33149,varname:node_4318,prsc:2|A-1129-OUT,B-9318-OUT;n:type:ShaderForge.SFN_Clamp01,id:1962,x:32235,y:33149,varname:node_1962,prsc:2|IN-4318-OUT;n:type:ShaderForge.SFN_Clamp01,id:4778,x:30993,y:32403,varname:node_4778,prsc:2|IN-9839-R;n:type:ShaderForge.SFN_Slider,id:193,x:30836,y:32289,ptovrint:False,ptlb:Strensh,ptin:_Strensh,varname:node_194,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Multiply,id:7666,x:31214,y:32403,varname:node_7666,prsc:2|A-4778-OUT,B-193-OUT;n:type:ShaderForge.SFN_Multiply,id:1642,x:31415,y:32403,varname:node_1642,prsc:2|A-7666-OUT,B-7666-OUT;n:type:ShaderForge.SFN_SceneColor,id:8423,x:31869,y:32428,varname:node_8423,prsc:2|UVIN-1908-OUT;n:type:ShaderForge.SFN_Clamp01,id:6327,x:32052,y:32428,varname:node_6327,prsc:2|IN-8423-RGB;n:type:ShaderForge.SFN_Tex2d,id:9839,x:30779,y:32403,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_9839,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b98f17094a0e66a469f38a1607e95553,ntxv:2,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:6257,x:31415,y:32244,varname:node_6257,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:1908,x:31598,y:32403,varname:node_1908,prsc:2|A-6257-V,B-1642-OUT;n:type:ShaderForge.SFN_Add,id:2462,x:32691,y:32767,varname:node_2462,prsc:2|A-7577-OUT,B-2393-OUT;n:type:ShaderForge.SFN_Vector1,id:7373,x:31658,y:32895,varname:node_7373,prsc:2,v1:0.7;n:type:ShaderForge.SFN_Subtract,id:76,x:31842,y:32745,varname:node_76,prsc:2|A-4171-Z,B-7373-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6293,x:31690,y:33300,ptovrint:False,ptlb:Opacity power,ptin:_Opacitypower,varname:node_6293,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_ValueProperty,id:9318,x:31878,y:33300,ptovrint:False,ptlb:Opacity strench,ptin:_Opacitystrench,varname:node_9318,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:50;n:type:ShaderForge.SFN_SwitchProperty,id:7577,x:32235,y:32428,ptovrint:False,ptlb:Use distortion?,ptin:_Usedistortion,varname:node_7577,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-2168-OUT,B-6327-OUT;n:type:ShaderForge.SFN_Vector1,id:2168,x:32052,y:32346,varname:node_2168,prsc:2,v1:0;proporder:6074-797-193-9839-6293-9318-7577;pass:END;sub:END;*/

Shader "Shader Forge/Slash" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (0,0.5019608,1,1)
        _Strensh ("Strensh", Range(0, 2)) = 1
        _Noise ("Noise", 2D) = "black" {}
        _Opacitypower ("Opacity power", Float ) = 4
        _Opacitystrench ("Opacity strench", Float ) = 50
        [MaterialToggle] _Usedistortion ("Use distortion?", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform float _Strensh;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _Opacitypower;
            uniform float _Opacitystrench;
            uniform fixed _Usedistortion;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float node_7666 = (saturate(_Noise_var.r)*_Strensh);
                float node_4771 = (i.uv0.g*1.0);
                float2 node_5777 = ((float2(i.uv0.r,node_4771)*node_4771)+(i.uv0.b-0.7)*float2(0,1));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_5777, _MainTex));
                float node_1962 = saturate((pow((1.0 - node_4771),_Opacitypower)*_Opacitystrench));
                float3 emissive = (lerp( 0.0, saturate(tex2D( _GrabTexture, (i.uv0.g+(node_7666*node_7666))).rgb), _Usedistortion )+(_MainTex_var.rgb*i.vertexColor.rgb*_TintColor.rgb*2.0*node_1962));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_MainTex_var.a*i.vertexColor.a*_TintColor.a*node_1962));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
