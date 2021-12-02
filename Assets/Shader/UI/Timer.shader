Shader "Custom/UI/Disolve"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}

        _DisolveMaske ("Disolve Texture", 2D) = "white" {}
        _DisolveScaleA ("Disolve Scale A", range(0, 50)) = 2
        _DisolveSpeedA ("Scroll Speed A", range(0, 2)) = 2

        _DisolveScaleB ("Disolve Scale B", range(0, 50)) = 2
        _DisolveSpeedB ("Scroll Speed B", range(0, 2)) = 2

        _T ("Time", range(0, 1)) = 0

        _Color ("Tint", Color) = (1,1,1,1)
        _ColorA ("Tint A", Color) = (1,1,1,1)

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            sampler2D _DisolveMaske;
            fixed4 _Color, _ColorA;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            fixed _DisolveScaleA, 
            _DisolveScaleB,
            _T, 
            _DisolveSpeedA, 
            _DisolveSpeedB;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                OUT.color = v.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
                half disolveTexValue = pow(
                    saturate(
                    tex2D(_DisolveMaske, (IN.texcoord - fixed2(0, _Time.x * _DisolveSpeedA)) * _DisolveScaleA).r *
                    tex2D(_DisolveMaske, (IN.texcoord - fixed2(0, _Time.x * _DisolveSpeedB)) * _DisolveScaleB).r * 2), .8);

                //disolveShader += _UVOffset * 20 + (1 - IN.texcoord.y * 20);
                //disolveShader = step( .5, disolveShader);

                fixed disolveValue = disolveTexValue + (lerp(.71, .824, _T) * 20 + (1 - IN.texcoord.y * 20));

                fixed valueA = step(.5, disolveValue);
                fixed valueB = step(disolveValue, .55) * step(.5, disolveValue);



                return color * valueA + valueB * _ColorA;
            }
        ENDCG
        }
    }
}