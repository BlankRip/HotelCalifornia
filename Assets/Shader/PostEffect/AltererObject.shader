Shader "Custom/PostEffects/AltererObject"
{
    HLSLINCLUDE

    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
    TEXTURE2D_SAMPLER2D(_PossesedTex, sampler_PossesedTex);
    float4 _Color;
    float _Blend;

    float4 Frag(VaryingsDefault i) : SV_Target
    {
        // test gray scale
        float alteredObjectArea = SAMPLE_TEXTURE2D(_PossesedTex, sampler_PossesedTex, i.texcoord);
        float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(1,0) * alteredObjectArea * _Blend);
        float luminance = dot(color.rgb, float3(0.2126729, 0.7151522, 0.0721750));
        return lerp(color,luminance.xxxx + _Color, saturate(alteredObjectArea.r));
    }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

            #pragma vertex VertDefault
            #pragma fragment Frag

            ENDHLSL
        }
    }
}