Shader "Shaders/Monochrome"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Saturation ("Saturation", Float) = 0
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
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Saturation;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);

				float s = _Saturation;
				const float R = 0.2126;
				const float G = 0.7152;
				const float B = 0.0722;
				float3 mono = float3(1.0, 1.0, 1.0) * (col.r * R + col.g * G + col.b * B);
				return float4(mono.r * (1-s) + col.r * s, mono.g  * (1-s) + col.g * s, mono.b  * (1-s) + col.b * s, col.a);
			}
			ENDCG
		}
	}
}
