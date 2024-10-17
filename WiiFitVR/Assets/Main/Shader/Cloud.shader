Shader "Custom/Cloud"
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
                // �e�N�X�`���J���[�̎擾
                float4 texColor = tex2D(_MainTex, i.uv);

                // ��ʒ�������̋����Ɋ�Â����̉e�����v�Z
                float2 screenCenter = float2(0.5, 0.5);
                float dist = distance(i.uv, screenCenter);

                // ���̌��ʂ�Z�W�Ɋ�Â��Čv�Z
                float fogAmount = smoothstep(0.0, 1.0, dist * _Density);

                // ����K�p
                texColor.rgb = lerp(texColor.rgb, _FogColor.rgb, fogAmount);

                return texColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}