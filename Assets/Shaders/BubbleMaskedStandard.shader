Shader "Bubble/MaskedStandard"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        _BubbleOrigin ("Bubble World Origin", vector) = (0,0,0,0)
        _BubbleRangeSqr ("Bubble Range Square", float) = 9999999999.0
    }
    SubShader
    {

        // MASKED FRONT FACING PASS //

        Tags { "RenderType"="Opaque" }
        LOD 200

        /*
        Stencil
        {
            Ref 0
            WriteMask 8
            Comp Always
            Fail Replace
        }
        */
        
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        float4 _BubbleOrigin;
        float _BubbleRangeSqr;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 deltaBubble = IN.worldPos - _BubbleOrigin;
            float dstSqr = dot(deltaBubble, deltaBubble);

            clip(_BubbleRangeSqr - dstSqr);

            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG


        // BACK FACING PASS //
        Pass
        {
            Tags { "RenderType"="Opaque" "Queue"="Geometry-100"}

            Cull Front
            ZWrite Off
            ColorMask 0

            Stencil
            {
                Ref 8
                WriteMask 8
                Pass Replace
            }

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
                float4 worldPos : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            float4 _BubbleOrigin;
            float _BubbleRangeSqr;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 deltaBubble = i.worldPos - _BubbleOrigin;
                float dstSqr = dot(deltaBubble, deltaBubble);

                clip(_BubbleRangeSqr - dstSqr);

                return fixed4(0, 0, 0, 1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
