Shader "Custom/PostEffects/BoxBlur"
{
    HLSLINCLUDE

    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
    float _Shift, _Mult;

    float4 Frag(VaryingsDefault i) : SV_Target
    {
        float aspectRatio = _ScreenParams.x/_ScreenParams.y;
        float4 color = 
        (SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(1,0) * _Shift)+
        SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(-1,0) * _Shift)+
        SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(0,1 * aspectRatio) * _Shift) +
        SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(0,-1 * aspectRatio) * _Shift) +
        SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(1,1 * aspectRatio) * _Shift * 0.707213) +
        SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(-1,1 * aspectRatio) * _Shift * 0.707213) +
        SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(1,-1 * aspectRatio) * _Shift * 0.707213) +
        SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(-1,-1 * aspectRatio) * _Shift * 0.707213) +
        SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord)) * 0.111111;
        return saturate(color * _Mult);
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