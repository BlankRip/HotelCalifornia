Shader "Custom/PostEffects/SmokeUp"
{
    HLSLINCLUDE

    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
    TEXTURE2D_SAMPLER2D(_TexB, sampler_TexB);
    TEXTURE2D_SAMPLER2D(_DistortionTex, sampler_DistortionTex);

    float _DistScale, _DistEffect, _DistSpeed;

    float4 Frag(VaryingsDefault i) : SV_Target
    {
        float aspectRatio = _ScreenParams.x/_ScreenParams.y;
        float2 distortionUV = SAMPLE_TEXTURE2D(_DistortionTex, sampler_DistortionTex, float2(i.texcoord.x * aspectRatio, i.texcoord.y - _Time.x * _DistSpeed) * _DistScale) * 2 - 1;
        distortionUV += SAMPLE_TEXTURE2D(_DistortionTex, sampler_DistortionTex, float2(i.texcoord.x * aspectRatio, i.texcoord.y) * _DistScale) * 2 - 1;
        distortionUV *= 0.5;
        float col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + distortionUV * _DistEffect).r;
        //col += SAMPLE_TEXTURE2D(_TexB, sampler_TexB, i.texcoord).r;
        return col;
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