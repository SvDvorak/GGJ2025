Shader "Bubble/Glow"
{
    Properties
    {
        _Color ("Main Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct v2f
            {
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 normal : TEXCOORD1;
            };

            fixed4 _Color;
            float3 _ViewDir;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = float4(v.normal.x, v.normal.y, v.normal.z, 0);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float angle = dot(i.normal.xyz, -_ViewDir);
                angle = pow(angle, 4);
                fixed4 col = _Color;
                col.a *= angle;
                return col;
            }
            ENDCG
        }
    }
}
