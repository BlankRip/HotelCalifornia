Shader "Custom/PostEffects/GlitchEffect"
{
    HLSLINCLUDE

    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
    uniform TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);
    uniform float _ChromaShift;
    uniform float _DistanceFogEffect;
    uniform float4 _DistanceColor;

    float UVToColor(float2 uv)
    {
        return frac(sin(dot(uv + float2(_Time.x, 0),float2(12.9898,78.233)))*43758.5453123);
    }

    float4 Frag(VaryingsDefault i) : SV_Target
    {


        float depth = SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoord).r;
        depth = Linear01Depth(depth);
        depth = depth * _ProjectionParams.z;
        float shiftDist = saturate(1 - (depth * .1));

        float oneXPix = 1/_ScreenParams.x;
        float r = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(_ChromaShift * oneXPix * shiftDist, 0)).r;
        float g = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord).g;
        float b = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(-_ChromaShift * oneXPix * shiftDist, 0)).b;

        

        return float4(r,g,b, 1) + lerp(0, _DistanceColor, depth * _DistanceFogEffect);
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