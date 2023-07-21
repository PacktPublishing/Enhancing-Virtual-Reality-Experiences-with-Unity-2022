

Shader "Water/Ocean_03" {
    Properties {
        _WaterColor ("Water Color", Color) = (0.4926471,0.6011156,1,1)
        _MainTexture ("Main Texture", 2D) = "white" {}
        _FoamTexture ("Foam Texture", 2D) = "white" {}
        _AdditionalTexture ("Additional Texture", 2D) = "white" {}
        _Noise ("Noise", 2D) = "white" {}
        _Highlight ("Highlight", 2D) = "white" {}
        [Space(10)]
        [Space(10)]
        _FoamOpacity ("Foam Opacity", Range(0, 2)) = 1
        _Noiselevel ("Noise level", Range(0, 0.12)) = 0.02
        _WaterOrientation ("Water Orientation", Range(0, 4)) = 2
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _AdditionalTexture; uniform float4 _AdditionalTexture_ST;
            uniform float4 _WaterColor;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform sampler2D _FoamTexture; uniform float4 _FoamTexture_ST;
            uniform float _Noiselevel;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _FoamOpacity;
            uniform float _WaterOrientation;
            uniform sampler2D _Highlight; uniform float4 _Highlight_ST;
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
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
////// Lighting:
////// Emissive:
                float4 node_4194 = _Time;
                float node_1855_ang = _WaterOrientation;
                float node_1855_spd = 1.0;
                float node_1855_cos = cos(node_1855_spd*node_1855_ang);
                float node_1855_sin = sin(node_1855_spd*node_1855_ang);
                float2 node_1855_piv = float2(0.5,0.5);
                float2 node_1855 = (mul(i.uv0-node_1855_piv,float2x2( node_1855_cos, -node_1855_sin, node_1855_sin, node_1855_cos))+node_1855_piv);
                float2 node_7411 = (node_1855+node_4194.g*float2(0,0.05));
                float4 _MainTexture_var = tex2D(_MainTexture,TRANSFORM_TEX(node_7411, _MainTexture));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(node_7411, _Noise));
                float node_4471 = _Noise_var.r;
                float2 node_8724 = lerp(node_1855,float2(node_4471,node_4471),_Noiselevel);
                float2 node_5898 = (node_8724+node_4194.g*float2(0,0.01));
                float4 _AdditionalTexture_var = tex2D(_AdditionalTexture,TRANSFORM_TEX(node_5898, _AdditionalTexture));
                float2 node_4898 = ((sceneUVs * 2 - 1).rg+node_4194.g*float2(0,0.02));
                float4 _Highlight_var = tex2D(_Highlight,TRANSFORM_TEX(node_4898, _Highlight));
                float2 node_2716 = (node_8724+node_4194.g*float2(0,0.02));
                float4 _FoamTexture_var = tex2D(_FoamTexture,TRANSFORM_TEX(node_2716, _FoamTexture));
                float3 emissive = saturate((1.0-(1.0-saturate((1.0-(1.0-saturate(min(_MainTexture_var.rgb,_WaterColor.rgb)))*(1.0-_AdditionalTexture_var.r))))*(1.0-saturate((saturate(( _FoamTexture_var.r > 0.5 ? (1.0-(1.0-2.0*(_FoamTexture_var.r-0.5))*(1.0-_Highlight_var.rgb)) : (2.0*_FoamTexture_var.r*_Highlight_var.rgb) ))*_FoamOpacity)))));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
   
}
