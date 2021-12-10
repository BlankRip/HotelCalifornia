Shader "Custom/PostEffects/Fog"
{
    HLSLINCLUDE

    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
    uniform TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);
    uniform float _DistanceFogEffect;
    uniform float _Knee;
    uniform float _StartOffset;
    uniform float4 _DistanceColor;


    float4 Frag(VaryingsDefault i) : SV_Target
    {


        float depth = SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoord).r;
        depth = Linear01Depth(depth);
        depth = depth * _ProjectionParams.z;

        float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
           

        return col + lerp(0, _DistanceColor, pow(saturate((depth * _DistanceFogEffect) - _StartOffset), _Knee));
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