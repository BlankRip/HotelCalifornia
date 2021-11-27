Shader "Custom/PostEffects/GhostVission"
{
    HLSLINCLUDE

    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
    TEXTURE2D_SAMPLER2D(_DistortionTex, sampler_DistortionTex);
    TEXTURE2D_SAMPLER2D(_ExcludeColor, sampler_ExcludeColor);

    uniform float 
    _StepDist, 
    _ValueControl, 
    _OutKnee, 
    _OutStrength, 
    _DistortionScale, 
    _DistortionStrength, 
    _DistortionSSoftness, 
    _DistortionSpeed,
    _ColorTightness;

    uniform int 
    _StepCount;

    float4 _GhostZoneColor;

    float4 Frag(VaryingsDefault i) : SV_Target
    {
        float4 excludeCol = SAMPLE_TEXTURE2D(_ExcludeColor, sampler_ExcludeColor, i.texcoord);
        // test gray scale
        float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
        float2 uvCent = i.texcoord.xy * 2 - 1;
        float2 blurVector = -uvCent;
        float luminance = dot(color.rgb, float3(0.2126729, 0.7151522, 0.0721750));

        float texRatio = _ScreenParams.x/_ScreenParams.y;
        float rawDistortionValue = SAMPLE_TEXTURE2D(_DistortionTex, sampler_DistortionTex, float2(((1 - i.texcoord.x) + _Time.x * _DistortionSpeed) * texRatio, i.texcoord.y - _Time.x * _DistortionSpeed) * _DistortionScale).x* 
        SAMPLE_TEXTURE2D(_DistortionTex, sampler_DistortionTex, float2((i.texcoord.x + _Time.x * _DistortionSpeed) * texRatio, (i.texcoord.y - _Time.x * 1.5 * _DistortionSpeed)) * _DistortionScale * 1.5).x;
        
        float distortionValue = pow( rawDistortionValue * _DistortionStrength, _DistortionSSoftness);

        float multiplier = 0;
        for(uint j = 0; j < _StepCount; j++){
            float pointValue = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + blurVector * saturate(1 - distortionValue) * j * _StepDist *(length(uvCent)));
            multiplier += (1 - j/_StepCount) * pow(pointValue, 3);
        }
        float ctrl = luminance;
        float4 colMain = lerp(luminance.xxxx * _GhostZoneColor, color, pow(ctrl, _ColorTightness));
        colMain += pow(multiplier * _ValueControl, _OutKnee) * _OutStrength;

        

        //return saturate(1 - distortionValue);
        return lerp(colMain, color, excludeCol.a);
        //return saturate(1 - length(uvCent)) * lerp(luminance.xxxx, color, pow(ctrl * 2, 2)) + pow(multiplier * _ValueControl, _OutKnee) * _OutStrength;
        //return luminance + multiplier * _ValueControl;
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