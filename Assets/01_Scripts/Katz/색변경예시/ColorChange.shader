Shader "Custom/ColorConverterSprite" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _RedColor("Red Color", Color) = (1, 0, 0, 1) //바꿀 컬러
        _GreenColor("Green Color", Color) = (0, 1, 0, 1) //대상 컬러
    }
        SubShader{
            Pass {
                Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
                ZWrite Off
                Blend SrcAlpha OneMinusSrcAlpha

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                float4 _RedColor;
                float4 _GreenColor;

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    // Get the color of the current pixel
                    fixed4 col = tex2D(_MainTex, i.uv);

                // If the pixel is red, change it to green
                //if (col.rgb == _RedColor.rgb) {
                //    col = _GreenColor;
                //}
                if (col.r > 0.59 && col.r < 0.6 && col.g < 0.01 && col.b > 0.99) {
                    col = fixed4(0, 1, 0, col.a);
                }

                return col;
            }
            ENDCG
        }
        }
            FallBack "Sprites/Default"
}