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
   /* 
        SubShader{
            Tags{ "RenderType" = "Transparent"    }
            Blend SrcAlpha OneMinusSrcAlpha
            LOD 200
            Pass{
                Cull Off
            }
            CGPROGRAM
            #pragma surface surf Standard vertex:vert fullforwardshadows alpha:fade
            #pragma target 3.0
            struct Input {
                float4 vertexColor;
                 float2 uv_MainTex;
            };

            void vert(inout appdata_full v, out Input o)
            {
                o.vertexColor = v.color;
            }

            sampler2D _MainTex;

            fixed4 _Color;
            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                o.Albedo = IN.vertexColor * tex2D(_MainTex, IN.uv_MainTex).rgb;
                o.Alpha = IN.vertexColor.w;
            }
            ENDCG
    }
        FallBack "Diffuse"
}*/