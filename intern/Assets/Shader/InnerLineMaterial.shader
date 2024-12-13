Shader "Custom/InnerLineMaterial"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _LineColor ("Line Color", Color) = (0, 0, 0, 1)
        _LineWidth ("Line Width", Float) = 0.05
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        Cull off
        Lighting off
        ZWrite off
        Fog { Mode off }

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
            float4 _LineColor;
            float _LineWidth;
        
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                fixed4 texColor = tex2D(_MainTex, uv);

                // インナーラインの条件
                float border = _LineWidth;
                if (uv.x < border || uv.y < border || uv.x > 1.0 - border || uv.y > 1.0 - border)
                {
                    // ラインカラーを返す
                    return _LineColor;
                }

                // 通常のスプライトカラーを返す
                return texColor;
            }
            ENDCG
        }
    }
}
