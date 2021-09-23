// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/Additive Smoke(Soft)" {
    Properties {
        _MainTex ("Particle Texture", 2D) = "white" {}
        _Distortion ("Distortion Texture", 2D) = "white" {}
        _FadeTightness ("Fade Tightness", Range(0.01,10)) = 1.0
        _InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
        _FogSpeed ("Fog Speed", Range(0.01,1)) = 1.0
    }

    Category {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
        Blend OneMinusDstColor One
        ColorMask RGB
        Cull Off Lighting Off ZWrite Off

        SubShader {
            Pass {

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 2.0
                #pragma multi_compile_particles
                #pragma multi_compile_fog

                #include "UnityCG.cginc"

                sampler2D _MainTex, _Distortion;
                fixed4 _TintColor;

                struct appdata_t {
                    float4 vertex : POSITION;
                    fixed4 color : COLOR;
                    fixed4 normal : NORMAL;
                    float2 texcoord : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct v2f {
                    float4 vertex : SV_POSITION;
                    fixed4 color : COLOR;
                    fixed3 wNormal : NORMAL0;
                    fixed3 wPos : NORMAL1;
                    fixed3 lPos : NORMAL2;
                    fixed3 vDir : TEXCOORD3;
                    float2 texcoord : TEXCOORD0;
                    UNITY_FOG_COORDS(1)
                    #ifdef SOFTPARTICLES_ON
                        float4 projPos : TEXCOORD2;
                    #endif
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                float4 _MainTex_ST;

                v2f vert (appdata_t v)
                {
                    v2f o;
                    UNITY_SETUP_INSTANCE_ID(v);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                    o.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                    v.vertex.y += (sin(((o.wPos.x * o.wPos.z * 0.2) * 0.1 +  _Time.x * 10) * 1) * 0.5 + 0.5) * 0.3 ;

                    o.lPos = v.vertex.xyz;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    #ifdef SOFTPARTICLES_ON
                        o.projPos = ComputeScreenPos (o.vertex);
                        COMPUTE_EYEDEPTH(o.projPos.z);
                    #endif
                    o.color = v.color;
                    o.vDir = UnityWorldSpaceViewDir(o.wPos);
                    o.wNormal = UnityObjectToWorldNormal(v.normal);
                    o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
                    UNITY_TRANSFER_FOG(o,o.vertex);
                    return o;
                }

                UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
                float _InvFade, _FadeTightness, _FogSpeed;

                float2 toPolar(float2 cartesian){
                    float distance = length(cartesian);
                    float angle = atan2(cartesian.y, cartesian.x);
                    return float2(angle / UNITY_TWO_PI, distance);
                }

                float2 RotateVec (float2 uv, float degrees)
                {
                    float alpha = degrees * UNITY_PI / 180.0;
                    float sina, cosa;
                    sincos(alpha, sina, cosa);
                    float2x2 m = float2x2(cosa, -sina, sina, cosa);
                    //return float3(mul(m, vertex.xz), vertex.y).xzy;
                    return mul(m, uv).xy;
                }

                fixed4 frag (v2f i) : SV_Target
                {
                    #ifdef SOFTPARTICLES_ON
                        float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
                        float partZ = i.projPos.z;
                        float fade = pow(saturate (_InvFade * (sceneZ-partZ)), _FadeTightness);
                        i.color.a *= fade * abs(dot(normalize(i.vDir), i.wNormal));
                    #endif

                    fixed2 distortionUV = i.wPos.xz * 0.05 + fixed2(_Time.y, -_Time.y) * _FogSpeed * 0.1;
                    fixed2 distOffset = (tex2D(_Distortion, distortionUV).xy) * 2 - 1;

                    fixed2 newUV = (i.wPos.xz * 0.1 + fixed2(_Time.y, _Time.y) * _FogSpeed) + distOffset * 0.1;
                    fixed2 newUV2 = (i.wPos.xz * 0.08 + fixed2(_Time.y, _Time.y) * _FogSpeed * 1.3) + RotateVec (distOffset, 30) * 0.1;
                    fixed2 newUV3 = (i.wPos.xz * 0.03 + fixed2(_Time.y, _Time.y) * _FogSpeed * 1.5) + RotateVec (distOffset, 60) * 0.1;

                    

                    half4 col = i.color * (tex2D(_MainTex, newUV ) + tex2D(_MainTex, newUV2) + tex2D(_MainTex, newUV3)) * 0.333 * 1.5;
                    col.rgb *= col.a;
                    UNITY_APPLY_FOG_COLOR(i.fogCoord, col, fixed4(0,0,0,0)); // fog towards black due to our blend mode
                    return col;
                }
                ENDCG
            }
        }
    }
}
