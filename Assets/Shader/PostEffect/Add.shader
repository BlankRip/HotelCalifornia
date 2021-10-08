Shader "Custom/PostEffects/Add"
{
    HLSLINCLUDE

    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
    TEXTURE2D_SAMPLER2D(_TexB, sampler_TexB);
    float4 Frag(VaryingsDefault i) : SV_Target
    {
        return saturate(SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord) + SAMPLE_TEXTURE2D(_TexB, sampler_MainTex, i.texcoord));
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