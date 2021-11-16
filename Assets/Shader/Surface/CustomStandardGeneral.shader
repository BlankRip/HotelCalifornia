Shader "Custom/CustomStandardGeneral"
{
    Properties
    {
        //_LightShadow ("Light Shadow", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _EmissionTex ("Emssion (RGB)", 2D) = "black" {}
        _BumpMap ("Bumpmap", 2D) = "bump" {}
     //   _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
        //_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
        _Glosiness ("Glosiness", Range(1,30)) = 2.0
        _GlossPower ("Gloss Power", Range(0,10)) = 0.3
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf SimpleLambert fullforwardshadows
        fixed3 vDir;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        uniform fixed4 _RimColor;
        uniform fixed _RimPower;
        //fixed _RimPower;
        fixed _Glosiness;
        fixed _GlossPower;
        uniform sampler2D _LightShadow, _EmissionTex;

        half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten) {
            
            fixed3 nLightDir = normalize(lightDir);
            half NdotL = dot(s.Normal, nLightDir);

            fixed3 H = normalize(lightDir + vDir);
            fixed intencity = saturate(pow(max(dot(s.Normal, H), 0) * 1, _Glosiness) * _GlossPower); 
            
            half4 c;
            fixed lightColorCTRL = saturate(((NdotL * 0.5 + 0.5) * atten * length(_LightColor0.rgb)));
            fixed3 lightColor = tex2D(_LightShadow, fixed2(lightColorCTRL, 0.5));
            c.rgb = ((s.Albedo + lightColor * _LightColor0.rgb * 0.5) * lightColor);
            c.rgb += _LightColor0 * intencity * max(NdotL, 0) * atten;
            c.a = s.Alpha;

            return c;
        }

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;

        struct Input
        {
            fixed2 uv_MainTex;
            fixed2 uv_BumpMap;
            fixed3 worldPos;
            fixed3 viewDir;
        };

        

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
        // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            vDir = -normalize(IN.worldPos - _WorldSpaceCameraPos);
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            //o.Albedo = pow(c.rgb, 2) + c.rgb;
            o.Albedo = c.rgb;
            o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
            half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
            o.Emission = _RimColor.rgb * pow (rim, _RimPower) + tex2D(_EmissionTex, IN.uv_MainTex) * 1.3;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
