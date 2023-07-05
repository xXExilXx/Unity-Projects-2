Shader "Custom/TrailShader"
{
    Properties
    {
        _TrailColor("Trail Color", Color) = (1, 1, 1, 1)
        _TrailLength("Trail Length", Range(0.1, 2.0)) = 0.5
        _TrailOffset("Trail Offset", Range(-1.0, 1.0)) = 0.0
    }

        SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }

        Blend SrcAlpha OneMinusSrcAlpha

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
                float4 vertex : SV_POSITION;
                float trailOffset : TEXCOORD1;
            };

            float _TrailLength;
            float _TrailOffset;
            fixed4 _TrailColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.trailOffset = _TrailOffset;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed alpha = smoothstep(_TrailLength - 0.02, _TrailLength, i.vertex.x + i.trailOffset);
                return _TrailColor * alpha;
            }
            ENDCG
        }
    }
}
