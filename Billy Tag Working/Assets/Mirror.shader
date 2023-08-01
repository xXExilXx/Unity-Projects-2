Shader "Custom/MirrorShader" {
    Properties {
        _MainTex ("Mirror Texture", 2D) = "white" {}
    }
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        struct Input {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;

        void surf (Input IN, inout SurfaceOutput o) {
            float2 reflectedUV = IN.uv_MainTex; // Initialize the reflected UV coordinates
            reflectedUV.y = 1.0 - reflectedUV.y; // Flip the UV vertically for reflection

            // Sample the reflected color from the mirror texture
            fixed4 reflectedColor = tex2D(_MainTex, reflectedUV);

            // Apply the reflected color to the output surface
            o.Albedo = reflectedColor.rgb;
            o.Alpha = reflectedColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
