Shader "Custom/TransparentVertex" {
    Properties{
          _MainTex("Main Texture", 2D) = "white" {}
    }
        
        SubShader{
          Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent"  }
         Blend SrcAlpha OneMinusSrcAlpha
        ZWrite On
         LOD 200
    CGPROGRAM
    #pragma surface surf NoLighting vertex:vert alpha:fade

            sampler2D _MainTex;
    struct Input {
        float4 vertColor;
        float2 uv_MainTex;
    };

    fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
    {
        fixed4 c;
        c.rgb = s.Albedo;
        c.a = s.Alpha;
        return c;
    }

    void vert(inout appdata_full v, out Input o) {
        UNITY_INITIALIZE_OUTPUT(Input, o);
        o.vertColor = v.color;
    }

    void surf(Input IN, inout SurfaceOutput o) {
        o.Albedo = IN.vertColor * tex2D(_MainTex, IN.uv_MainTex).rgb;
        o.Alpha = tex2D(_MainTex, IN.uv_MainTex).a;
    }
    ENDCG
    }
}