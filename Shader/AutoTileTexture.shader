Shader "Custom/AutoTileTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TileMultiplier ("Tile Multiplier", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Properties
            sampler2D _MainTex;
            float _TileMultiplier;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            // Vertex Shader
            v2f vert(appdata v)
            {
                v2f o;

                // 모델 매트릭스에서 오브젝트의 스케일 값을 가져옵니다.
                float3 scale = float3(
                    length(float3(unity_ObjectToWorld[0].xyz)),
                    length(float3(unity_ObjectToWorld[1].xyz)),
                    length(float3(unity_ObjectToWorld[2].xyz))
                );

                // UV 좌표를 스케일에 맞춰 조정하여 타일링을 적용합니다.
                o.uv = v.texcoord.xy * scale.xy * _TileMultiplier;

                // 포지션 설정
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            // Fragment Shader
            fixed4 frag(v2f i) : SV_Target
            {
                // 텍스처 샘플링
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
