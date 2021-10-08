Shader "Custom/AlteredItemArteRender"
{
    Properties
    {
        
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
                fixed4 vertex : POSITION;
                fixed2 uv : TEXCOORD0;
            };

            struct v2f
            {
                fixed4 vertex : SV_POSITION;
                fixed4 screenPos : TEXCOORD0;
                fixed depth : COLOR0;
            };

            uniform sampler2D _CameraDepthTexture;
            sampler2D _MainTex;
            fixed4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                o.depth = -(UnityObjectToViewPos(v.vertex).z
                * _ProjectionParams.w);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                fixed2 uv = i.screenPos.xy / i.screenPos.w;
                fixed camDepth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv);
                camDepth = Linear01Depth(camDepth);
                clip(0.000001 - (i.depth - camDepth));
                return 1;
            }
            ENDCG
        }
    }
}
