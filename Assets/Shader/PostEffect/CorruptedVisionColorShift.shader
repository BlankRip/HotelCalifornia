Shader "Custom/PostEffects/CorruptedVisionColorShift"
{
    HLSLINCLUDE

    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    uniform TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
    uniform TEXTURE2D_SAMPLER2D(_LookupTexture, sampler_LookupTexture);


    float4 Frag(VaryingsDefault i) : SV_Target
    {
        float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
        float luminance = dot(color.rgb, float3(0.2126729, 0.7151522, 0.0721750));
        float4 shiftColor = SAMPLE_TEXTURE2D(_LookupTexture, sampler_LookupTexture, float2(luminance, 0.5));
        return (shiftColor);
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