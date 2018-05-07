Shader "Unlit/UV"
{
	Properties
	{
		//_Origin("Origin", Float2) = (0,0)
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
				//float2 uv : TEXCOORD0;
				float3 normals : NORMAL;
			};

			struct v2f
			{
				//float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float3 normals : NORMAL;
			};

			//float2 _Origin;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				//o.uv = v.uv;
				o.normals = v.normals;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float3 n = i.normals * 0.5 + 0.5;
				float3 n2 = float3(n.x, 0.5, n.z)*n.y + (1-n.y) * float3(0.5, 1, 0.5);
				return float4(n2, 1);
				//float2 p = i.vertex.xy - _Origin.xy + float2(1,1))/2;
				//return float4(p.x, 0, p.y, 0);
			}
			ENDCG
		}
	}
}
