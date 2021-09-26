Shader "Custom/PostEffects/CorruptedVisionCombine"
{
    HLSLINCLUDE

    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    uniform TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
    uniform TEXTURE2D_SAMPLER2D(_BlurImage, sampler_BlurImage);
    uniform TEXTURE2D_SAMPLER2D(_GhostSmokeImage, sampler_GhostSmokeImage);
    uniform TEXTURE2D_SAMPLER2D(_DistortionTexture, sampler_DistortionTexture);

    uniform float _SelectImage, _VapourStrength;

    uniform float4 _VapourColor;

    float2 UV2Polar(float2 cartesian)
    {
        float distance = length(cartesian);
        float angle = atan2(cartesian.y, cartesian.x);
        return float2(angle / 6.283, distance);
    }


    float4 Frag(VaryingsDefault i) : SV_Target
    {
        float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
        float luminance = dot(color.rgb, float3(0.2126729, 0.7151522, 0.0721750));
        float4 shiftColor = SAMPLE_TEXTURE2D(_BlurImage, sampler_BlurImage, i.texcoord);
        float2 newUV = i.texcoord * 2 - 1;
        float radialGradient = length(newUV);
        float2 polarUV = UV2Polar(newUV);

        float2 distortionUV = 0.1 * (SAMPLE_TEXTURE2D(_DistortionTexture, sampler_DistortionTexture, i.texcoord + float2(_Time.x * 3, _Time.x)).xy * 2 - 1);

        float vapours = (
            (
                SAMPLE_TEXTURE2D(_GhostSmokeImage, sampler_GhostSmokeImage, distortionUV + float2(polarUV.x * 5, (polarUV.y - _Time.y))).x +
                SAMPLE_TEXTURE2D(_GhostSmokeImage, sampler_GhostSmokeImage, distortionUV + float2(polarUV.x * 2, (polarUV.y - _Time.y * 2))).x +
                SAMPLE_TEXTURE2D(_GhostSmokeImage, sampler_GhostSmokeImage, distortionUV * 1.5 + float2(polarUV.x * 10, (polarUV.y - _Time.y * 0.5) * 0.5)).x
                ) * 0.333
         * saturate(radialGradient - 0.5)) * _VapourStrength * 2;

        return lerp(color, shiftColor, saturate(radialGradient * 2 * _SelectImage)) + vapours * _VapourColor;
        ;
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