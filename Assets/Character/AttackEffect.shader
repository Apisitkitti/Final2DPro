Shader "Custom/AttackEffect" {
    Properties {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _Strength ("Strength", Range(0, 1)) = 1
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 texel = (1,1,1,1); // Define texel here

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _Mask;
            float _Strength;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Sample the main texture
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed4 finalColor = texColor * color * texel;
                // Sample the mask
                float maskValue = tex2D(_Mask, i.uv).r;

                // Apply the effect to the color
                fixed4 color = lerp(texColor, fixed4(1, 1, 1, 1), maskValue * _Strength);

                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
