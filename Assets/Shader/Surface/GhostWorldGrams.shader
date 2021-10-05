Shader "Custom/GhostWorldGrams"
{
    Properties
    {
        _Color ("Color", color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _DistUVTex ("UV Dist Tex", 2D) = "black" {}
        _DistTex ("Distortion", 2D) = "black" {}
        _DistScale("Distortion Scale", Range(0, 10)) = 0.5
        _DistSpeedX("Distortion Speed X", Range(-10, 10)) = 0.5
        _DistSpeedY("Distortion Speed Y", Range(-10, 10)) = 0.5
        _ClipRange("Clip Range", Range(0, 3)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Blend One One
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex, _DistTex, _DistUVTex;
            float4 _MainTex_ST, _Color;
            fixed _ClipRange, _DistScale, _DistSpeedX, _DistSpeedY;

            v2f vert (appdata v)
            {
                v2f o;
                fixed3 wPos =  sin(mul(unity_ObjectToWorld, fixed4(0,0,0,1)).xyz * 10000);
                fixed distortion =  tex2Dlod(_DistTex, fixed4(v.uv * _DistScale + fixed2((_Time.x + wPos.y) * _DistSpeedX, (_Time.x + wPos.y) * _DistSpeedY), 0, 0));
                v.vertex.x += distortion * 0.01;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            inline fixed ColorSqr(fixed3 col){
                return col.x * col.x + col.y * col.y + col.z * col.z;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                //clip(ColorSqr(col.xyz) - _ClipRange);
                fixed2 disUV = tex2D(_DistUVTex, i.uv + fixed2(_Time.x * 2 * _DistSpeedX, _Time.x * 2.3 * _DistSpeedY)) * 2 - 1;
                col += tex2D(_MainTex, i.uv + disUV * 0.03) * tex2D(_DistTex, (i.uv + 2) + fixed2(_Time.x * 2 * _DistSpeedX, _Time.x * 2.3 * _DistSpeedY) + disUV * 0.03).r;

                // apply fog
               // UNITY_APPLY_FOG(i.fogCoord, col);



                return col * _Color;
            }
            ENDCG
        }
    }
}
