Shader "Custom/CloudEffect"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _FogColor ("Fog Color", Color) = (1,1,1,1)
        _Density ("Fog Density", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            uniform sampler2D _MainTex;
            uniform float4 _FogColor;
            uniform float _Density;

            float4 frag(v2f_img i) : COLOR
            {
                // テクスチャカラーの取得
                float4 texColor = tex2D(_MainTex, i.uv);

                // 画面中央からの距離に基づく霧の影響を計算
                float2 screenCenter = float2(0.5, 0.5);
                float dist = distance(i.uv, screenCenter);

                // 霧の効果を濃淡に基づいて計算
                float fogAmount = smoothstep(0.0, 1.0, dist * _Density);

                // 霧を適用
                texColor.rgb = lerp(texColor.rgb, _FogColor.rgb, fogAmount);

                return texColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
