Shader "Custom/PostEffects/BlurHorizontal"
{
	HLSLINCLUDE

		#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

		TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
		float _Shift;

        #define correctionAmount 0.845;

		float4 HorizontalBlur(VaryingsDefault i) : SV_Target
		{
			float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord) * 0.16;
			color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(2.0 * _Shift, 0)) * 0.25;
			color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(3.0 * _Shift, 0)) * 0.05;
			color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(4.0 * _Shift, 0)) * 0.09;
			color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(5.0 * _Shift, 0)) * 0.12;

			color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(-2.0 * _Shift, 0)) * 0.25;
			color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(-3.0 * _Shift, 0)) * 0.05;
			color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(-4.0 * _Shift, 0)) * 0.09;
			color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + float2(-5.0 * _Shift, 0)) * 0.12;

		return color * correctionAmount;
	}

		ENDHLSL

		SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			HLSLPROGRAM

				#pragma vertex VertDefault
				#pragma fragment HorizontalBlur

			ENDHLSL
		}
	}
}