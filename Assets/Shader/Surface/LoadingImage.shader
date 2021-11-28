Shader "Custom/LoadingImage"
{
    Properties
    {
        [HDR]_Color ("Color", color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST, _Color;
            uniform fixed2 _Points[100];
            uniform int _PointCount;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed SqrDist(fixed2 pt)
            {
                return pt.x * pt.x + pt.y * pt.y;
            }

            fixed DrawLine(fixed2 p1, fixed2 p2, fixed2 uv, fixed thickness){
                fixed2 p12 = p2 - p1;
                fixed2 p1uv = uv - p1;
                fixed dotPUV = saturate(dot(p12, p1uv) / dot(p12, p12));
                fixed2 hVec = p1uv - (p12 * dotPUV);
                return step(SqrDist(hVec), pow(thickness,2));
            }

            fixed4 frag (v2f i) : SV_Target
            {
                int col = 0;
                for(int n = 1; n < _PointCount; n++)
                {
                    col = saturate(DrawLine(_Points[n - 1], _Points[n], i.uv, .02));
                    if(col > 0) return col * _Color;
                }
                clip(-1);
                return 0;
            }
            ENDCG
        }
    }
}
