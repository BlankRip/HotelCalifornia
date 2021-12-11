Shader "Custom/PostEffects/Fog"
{
    HLSLINCLUDE

    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
    uniform TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);
    uniform float _DistanceFogEffectGeneral;
    uniform float _KneeGeneral;
    uniform float _StartOffsetGeneral;
    uniform float4 _DistanceColorGeneral;


    float4 Frag(VaryingsDefault i) : SV_Target
    {


        float depth = SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoord).r;
        depth = Linear01Depth(depth);
        depth = depth * _ProjectionParams.z;

        float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
           

        return col + lerp(0, _DistanceColorGeneral, pow(saturate((depth * _DistanceFogEffectGeneral) - _StartOffsetGeneral), _KneeGeneral));
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