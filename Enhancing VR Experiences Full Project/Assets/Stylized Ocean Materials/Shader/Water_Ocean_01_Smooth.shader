Shader "Water/Ocean_01_Smooth" {
    Properties {
    [Header]
        _WaterColor ("Water Color", Color) = (0.4926471,0.6011156,1,1)
        _MainTexture ("Main Texture", 2D) = "white" {}
        _FoamTexture ("Foam Texture", 2D) = "white" {}
        _Addittionalfoam ("Addittional foam", 2D) = "white" {}
        _Noise ("Noise", 2D) = "white" {}
        [Space(10)]
        [Space(10)]

        _WaterOpacity ("Water Opacity", Range(0, 1)) = 0.5
        _FoamOpacity ("Foam Opacity", Range(0, 2)) = 2
        [Space(10)]
        [Space(10)]

         _WaterDepth ("Water Depth", Float ) = 10
         [Space(10)]
         [Space(10)]

        _EdgeFoamLevel ("Edge Foam Level", Float ) = 2
        _EdgeFoamLevelDestination ("Edge Foam Level Destination", Float ) = 3
        _EdgeFoamPower ("Edge Foam Power", Range(0, 1)) = 1
        [Space(10)]

        _Noiselevel ("Noise level", Range(0, 0.12)) = 0.1
        [Space(10)]


         _WaterOrientation ("Water Orientation", Range(0, 4)) = 2
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform sampler2D _Addittionalfoam; uniform float4 _Addittionalfoam_ST;
            uniform float4 _WaterColor;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform sampler2D _FoamTexture; uniform float4 _FoamTexture_ST;
            uniform float _WaterOpacity;
            uniform float _Noiselevel;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _FoamOpacity;
            uniform float _WaterDepth;
            uniform float _EdgeFoamLevel;
            uniform float _WaterOrientation;
            uniform float _EdgeFoamLevelDestination;
            uniform float _EdgeFoamPower;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 projPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);


                float4 node_8281 = _Time;
                float node_1855_ang = _WaterOrientation;
                float node_1855_spd = 1.0;
                float node_1855_cos = cos(node_1855_spd*node_1855_ang);
                float node_1855_sin = sin(node_1855_spd*node_1855_ang);
                float2 node_1855_piv = float2(0.5,0.5);
                float2 node_1855 = (mul(i.uv0-node_1855_piv,float2x2( node_1855_cos, -node_1855_sin, node_1855_sin, node_1855_cos))+node_1855_piv);
                float2 node_9870 = node_1855;
                float2 node_7411 = (node_9870+node_8281.g*float2(0,0.05));
                float4 _MainTexture_var = tex2D(_MainTexture,TRANSFORM_TEX(node_7411, _MainTexture));
                float2 node_5898 = (node_9870+node_8281.g*float2(0,0.01));
                float4 _Addittionalfoam_var = tex2D(_Addittionalfoam,TRANSFORM_TEX(node_5898, _Addittionalfoam));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float node_4471 = _Noise_var.r;
                float2 node_2716 = (lerp(node_1855,float2(node_4471,node_4471),_Noiselevel)+node_8281.g*float2(0,0.02));
                float4 _FoamTexture_var = tex2D(_FoamTexture,TRANSFORM_TEX(node_2716, _FoamTexture));
                float4 node_9546 = _Time;
                float node_7739 = (saturate(( (1.0 - saturate(saturate((sceneZ-partZ)/lerp(_EdgeFoamLevel,_EdgeFoamLevelDestination,clamp(sin(node_9546.g),-1,1))))) > 0.5 ? (1.0-(1.0-2.0*((1.0 - saturate(saturate((sceneZ-partZ)/lerp(_EdgeFoamLevel,_EdgeFoamLevelDestination,clamp(sin(node_9546.g),-1,1)))))-0.5))*(1.0-_Noise_var.r)) : (2.0*(1.0 - saturate(saturate((sceneZ-partZ)/lerp(_EdgeFoamLevel,_EdgeFoamLevelDestination,clamp(sin(node_9546.g),-1,1)))))*_Noise_var.r) ))*_EdgeFoamPower);
                float3 emissive = saturate((1.0-(1.0-saturate(max(saturate(( _WaterColor.rgb > 0.5 ? (1.0-(1.0-2.0*(_WaterColor.rgb-0.5))*(1.0-saturate((_MainTexture_var.rgb*_Addittionalfoam_var.rgb)))) : (2.0*_WaterColor.rgb*saturate((_MainTexture_var.rgb*_Addittionalfoam_var.rgb))) )),saturate((_FoamTexture_var.rgb*_FoamOpacity)))))*(1.0-(float3(1,1,1)*node_7739))));
                float3 finalColor = emissive;
                return fixed4(finalColor,saturate((1.0-(1.0-saturate((1.0-(1.0-saturate((1.0-(1.0-_FoamTexture_var.r)*(1.0-_WaterOpacity))))*(1.0-saturate((sceneZ-partZ)/_WaterDepth)))))*(1.0-node_7739))));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
