// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Unlit alpha-cutout shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Oni/UnlitCutoutUnmask" {
Properties {
    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    _MaskTex ("Base Mask Texture", 2D) = "white" {}
    _Cutoff ("Alpha cutoff", Range(0, 1)) = 0.5
    _UnmaskSlider ("Unmask Slider", Range(0, 1)) = 0.0
    _Color ("Color", Color) = (1,1,1,1)
}
SubShader 
{
    Tags 
    {
        "Queue"="Transparent"
        "IgnoreProjector"="True" 
        "RenderType"="Transparent"
    }
    
    LOD 100

    Lighting Off
    ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha

    Pass {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata_t 
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _MaskTex;            
            fixed _Cutoff;         
            fixed4 _Color;
            fixed _UnmaskSlider;

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);

                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = i.color * tex2D(_MainTex, i.texcoord);
                fixed4 maskCol = tex2D(_MaskTex, i.texcoord);                
                clip(maskCol.rgb - _Cutoff);
                clip(col.a - 0.001);
                UNITY_APPLY_FOG(i.fogCoord, col);

                // col.a = col.a * (maskCol.rgb) * smoothstep(0.0, _FadeSlider, fadeMaskCol.rgba);
                // col.a = col.a * (1 - (maskCol.rgb));
                col.a = col.a * smoothstep(-0.1, _UnmaskSlider, 1 - (maskCol.rgb));
                return col;
            }
        ENDCG
    }
}

}