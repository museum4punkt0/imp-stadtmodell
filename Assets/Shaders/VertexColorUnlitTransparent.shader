
Shader "Custom/TransparentVertexFrag" {
	Properties{
		  _MainTex("Main Texture", 2D) = "white" {}
	}

SubShader{
	Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent"  }
	Blend SrcAlpha OneMinusSrcAlpha
	ZWrite On
	LOD 200
	Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct v2f
			{
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR0;
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata_full v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				o.color = v.color;
				return o;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = pow(tex2D(_MainTex, i.uv) * i.color, float4(2.2, 2.2, 2.2, 1));
				return col;
			}

			ENDCG
		}
	}
}
