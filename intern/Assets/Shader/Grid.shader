Shader "Custom/Grid"
{
    Properties
    {
        _MainTex ("texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags {"RenderType"="Opaque"}
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
        
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }


            // ここまでテンプレート
            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv;
                uv.x = i.uv.x * 10;// 横軸の分割
                uv.y = i.uv.y * 20;// 縦軸の分割
                float2 gv = frac(uv);
                // smoothstep(現在の値,目的の値 ,割合 )
                float edge_x = smoothstep(0.90, 1, gv.x);
                float edge_y = smoothstep(0.90, 1, gv.y);
                float value = edge_x > edge_y ? edge_x : edge_y;
                return float4(value, value, value, 1.0);
            }
            ENDCG
        }
    }
}
