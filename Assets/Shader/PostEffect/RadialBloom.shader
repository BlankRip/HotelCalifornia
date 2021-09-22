Shader "PEFX/RadialBloom"
{
    HLSLINCLUDE

    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);

    uniform float _StepDist, _ValueControl, _OutKnee, _OutStrength;

    uniform int 
    _StepCount;

    float4 _GhostZoneColor;

    float4 Frag(VaryingsDefault i) : SV_Target
    {
        // test gray scale
        float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
        float2 uvCent = i.texcoord.xy * 2 - 1;
        float2 blurVector = -uvCent;
        float luminance = dot(color.rgb, float3(0.2126729, 0.7151522, 0.0721750));

        float multiplier = 0;
        
        for(uint j = 0; j < _StepCount; j++){
            float pointValue = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + blurVector * j * _StepDist *(length(uvCent)));
            multiplier += (1 - j/_StepCount) * pow(pointValue, 3);
        }
        float ctrl = luminance;
        float4 colMain = lerp(luminance.xxxx * _GhostZoneColor, color * color, pow(ctrl, 2));
        colMain += pow(multiplier * _ValueControl, _OutKnee) * _OutStrength;

        return colMain;
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