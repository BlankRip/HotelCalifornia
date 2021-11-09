Shader "Vishnu/ToonGlass"
{
	Properties
	{
		[HideInInspector] _MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
		_EdgeColor("Edge Color", Color) = (1, 1, 1, 1)
		_EdgeThickness("Silouette Dropoff Rate", float) = 1.0
		_Ramp("Ramp Texture", 2D) = "white" {}
		_AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
		_SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
		_Glossiness("Glossiness", Range(0, 1024)) = 32
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_RimAmount("Rim Amount", Range(0, 1)) = 0.716
		_RimThreshold("Rim Threshold", Range(0, 1)) = 0.1		
		_Bands("Bands", Range(0, 0.25)) = 0.15
	}
	
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
		}

		Pass
		{
			Cull Off
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha // standard alpha blending

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
            #pragma multi_compile_fwdbase

			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			// Properties
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _EdgeThickness;
			float4	_Color;
			float4	_EdgeColor;
			sampler2D _Ramp;
			float4 _Ramp_ST;
			float4 _AmbientColor;
			float4 _SpecularColor;
			float _Glossiness;		
			float4 _RimColor;
			float _RimAmount;
			float _RimThreshold;	
			float _Bands;	

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float3 texCoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 normal : NORMAL;
				float3 texCoord : TEXCOORD0;
				float3 viewDir : TEXCOORD1;
                SHADOW_COORDS(2)
			};

			v2f vert(appdata o)
			{
				v2f output;

				// convert input to world space
				output.pos = UnityObjectToClipPos(o.vertex);
				float3 normal = o.normal;
				output.normal = normalize(mul(normal, unity_WorldToObject).xyz);
				output.viewDir = normalize(_WorldSpaceCameraPos - mul(unity_ObjectToWorld, o.vertex).xyz);

				output.texCoord = o.texCoord;
                TRANSFER_SHADOW(output)

				return output;
			}

			float4 frag(v2f i) : COLOR 
			{
				float3 normal = normalize(i.normal);
				float3 viewDir = normalize(i.viewDir);

				float NdotL = dot(_WorldSpaceLightPos0, normal);
				float2 uv = float2(1 - (NdotL * _Bands + _Bands), _Bands);
				float4 bandSample = tex2D(_Ramp, uv);
				
				float shadow = SHADOW_ATTENUATION(i);
				float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);

				float4 light = lightIntensity * _LightColor0;
				
				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
				float NdotH = dot(normal, halfVector);
				float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
				float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
				float4 specular = specularIntensitySmooth * _SpecularColor;		

				float rimDot = 1 - dot(viewDir, normal);
				float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
				rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
				float4 rim = rimIntensity * _RimColor;

				float4 sample = tex2D(_MainTex, uv);				
				// sample texture for color
				float4 texColor = tex2D(_MainTex, i.texCoord.xy);

				// apply silouette equation based on how close normal is to being perpendicular to view vector
				// dot product is smaller the smaller the angle bw the vectors is
				// close to edge = closer to 0
				// far from edge = closer to 1
				float edgeFactor = abs(dot(i.viewDir, i.normal));

				// apply edgeFactor to Albedo color & EdgeColor
				float oneMinusEdge = 1.0 - edgeFactor;
				float3 rgb = (_Color.rgb * edgeFactor) + (_EdgeColor * oneMinusEdge);
				rgb = min(float3(1, 1, 1), rgb); // clamp to real color vals
				rgb = rgb * texColor.rgb;

				// apply edgeFactor to Albedo transparency & EdgeColor transparency
				// close to edge = more opaque EdgeColor & more transparent Albedo 
				float opacity = min(1.0, _Color.a / edgeFactor);

				// opacity^thickness means the edge color will be near 0 away from the edges
				// and escalate quickly in opacity towards the edges
				opacity = pow(opacity, _EdgeThickness);
				opacity = opacity * texColor.a;

				float4 output = float4(rgb, opacity);
				output *= (light + _AmbientColor + specular + rim) * _Color * sample * bandSample;
				return output;
			}
			ENDCG
		}
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}

}