Shader "Custom/GhostFreeZone"
{
    Properties
    {
        _DecalTex("Decal Texture", 2D) = "white"{}
        _GlowLookupTex ("Glow Gradient", 2D) = "white" {}
        _ScanShift ("Shift", Range(0,3)) = 0.01
        
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Geometry+1"}
        ZWrite Off
        ZTest greater
        Cull front
        //Blend SrcAlpha OneMinusSrcAlpha
        //Blend One OneMinusSrcAlpha
        Blend One One
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                fixed4 vertex : POSITION;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                fixed4 vertex : SV_POSITION;
                fixed4 screenPosition : NORMAL0;
                fixed3 viewDir : NORMAL1;
                fixed3 vDir : NORMAL2;
                fixed3 NvDir : NORMAL3;
                fixed4 color : COLOR;
            };

            uniform sampler2D _DecalDisolveTex;
            sampler2D 
            _CameraDepthTexture, 
            _DecalTex,
            _CameraDepthNormalsTexture,
            _GlowLookupTex;

            fixed _ScanShift;
            uniform float4x4 _CamToWorld;

            fixed SqrMag(fixed3 v){
                return v.x * v.x + v.y * v.y + v.z * v.z;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPosition = ComputeScreenPos(o.vertex);
                fixed3 wPos = mul(unity_ObjectToWorld, v.vertex);
                o.viewDir = normalize(mul(UNITY_MATRIX_MV, v.vertex).xyz); // get normalized view dir
                o.viewDir /= o.viewDir.z; // rescale vector so z is 1.0
                o.vDir = UnityWorldSpaceViewDir(wPos);
                o.NvDir = -normalize(o.vDir);
                o.color = v.color;
                return o;
            }


            fixed4 frag (v2f i) : SV_Target
            {
                fixed2 textureCoordinate = i.screenPosition.xy / i.screenPosition.w;
                fixed depth = LinearEyeDepth(tex2D(_CameraDepthTexture, textureCoordinate).r);
                fixed3 wDPos = _WorldSpaceCameraPos + i.NvDir * depth * length(i.viewDir);

                half3 normal;
                float depth1;

                DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, textureCoordinate), depth1, normal);
                fixed3 newNormal = mul( (float3x3)_CamToWorld, normal);


                fixed texScale = 0.1;

                fixed surfaceEffect = 
                tex2D(_DecalTex, fixed2(wDPos.x, wDPos.z) * texScale).r * abs(newNormal.y) +
                tex2D(_DecalTex, fixed2(wDPos.x, wDPos.y) * texScale).r * abs(newNormal.z) +
                tex2D(_DecalTex, fixed2(wDPos.z, wDPos.y) * texScale).r * abs(newNormal.x);

                fixed3 oPos = mul(unity_WorldToObject, fixed4(wDPos, 1)) * _ScanShift;
                clip(step(abs(oPos.x), 1) * step(abs(oPos.z), 1) * step(abs(oPos.y), 1) - 0.01);
                return tex2D(_GlowLookupTex, fixed2(pow(surfaceEffect, 0.9), 0.5)); 
            }
            ENDCG
        }
    }
}
