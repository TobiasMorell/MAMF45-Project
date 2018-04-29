Shader "Unlit/GrassShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_BendTex ("Bend Texture", 2D) = "white" {}
		_GrassWidth("Grass Width", Float) = 0.1
		_GrassHeight("Grass Height", Float) = 0.2
		_FieldSize("Field Size", Float) = 10
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CULL OFF
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma geometry geom
			// make fog work
			#pragma multi_compile_fog

			#pragma target 4.0
			
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			sampler2D _BendTex;
			float4 _MainTex_ST;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 color : TEXCOORD1;
			};

			struct v2g
			{
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float3 color : TEXCOORD1;
			};

			struct g2f 
			{
				float4 position : SV_POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 color : TEXCOORD1;

			};

			float _GrassWidth;
			float _GrassHeight; 
			float _FieldSize;
			
			float rand(float2 co){
				return frac(sin(dot(co.xy ,float2(12.9898,78.233))) * 43758.5453);
			}

			v2g vert (appdata v)
			{
				v2g o;
				o.vertex = v.vertex;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.normal = v.normal;
				o.color = v.color;

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			[maxvertexcount(7)]
			void geom(point v2g IN[1], inout TriangleStream<g2f> output) {
				float3 v0 = IN[0].vertex.xyz;

				float3 camDir = UNITY_MATRIX_IT_MV[2].xyz;

				float3 faceNormal = float3(camDir.x,0,camDir.z);
				float3 perpendicular = cross(faceNormal, IN[0].normal);
				
				float3 color = IN[0].color;
				
				float4 bend = float4(0,1,0,0); //tex2Dlod(_BendTex, float4(v0.x/_FieldSize + 0.5, v0.z/_FieldSize + 0.5, 0, 0));

				g2f OUT;
				
				float3 height = (rand(IN[0].vertex.xy) * 0.5 + 0.75 ) * _GrassHeight;
				float3 tip = float3(rand(IN[0].vertex.xy) - 0.5, 0, rand(IN[0].vertex.xy + float2(1, 1)) - 0.5) * _GrassWidth;

				//Lower right
				OUT.position = UnityObjectToClipPos(v0 + perpendicular * _GrassWidth/2);
				OUT.normal = faceNormal;
				OUT.color = color;
				OUT.uv = float2(1, 0);
				output.Append(OUT);

				OUT.position = UnityObjectToClipPos(v0 - perpendicular * _GrassWidth/2);
				OUT.normal = faceNormal;
				OUT.color = color;
				OUT.uv = float2(0, 0);
				output.Append(OUT);

				OUT.position = UnityObjectToClipPos(v0 + bend.xyz * height/3 + perpendicular * _GrassWidth/2);
				OUT.normal = faceNormal;
				OUT.color = color;
				OUT.uv = float2(1, 0.3);
				output.Append(OUT);
				
				//Lower left
				OUT.position = UnityObjectToClipPos(v0 + bend.xyz * height/3 - perpendicular * _GrassWidth/2);
				OUT.normal = faceNormal;
				OUT.color = color;
				OUT.uv = float2(0, 0.3);
				output.Append(OUT);
						
				//Middle right
				OUT.position = UnityObjectToClipPos(v0 + bend.xyz * height*3/4 + perpendicular * _GrassWidth/3 + tip/2);
				OUT.normal = faceNormal;
				OUT.color = color;
				OUT.uv = float2(1, 0.7);
				output.Append(OUT);

				//Middle left
				OUT.position = UnityObjectToClipPos(v0 + bend.xyz * height*3/4 - perpendicular * _GrassWidth/3 + tip/2);
				OUT.normal = faceNormal;
				OUT.color = color;
				OUT.uv = float2(0, 0.7);
				output.Append(OUT);

				//Top
				OUT.position = UnityObjectToClipPos(v0 + bend.xyz * height + tip);
				OUT.normal = float3(0,1,0);
				OUT.color = color;
				OUT.uv = float2(0.5, 1);
				output.Append(OUT);
								
				//Hat
				/*
				OUT.position = UnityObjectToClipPos(v0 + IN[0].normal * height*3/4 - perpendicular * _GrassWidth/3 - faceNormal * _GrassWidth/10);
				OUT.normal = float3(0,1,0);
				OUT.color = color;
				OUT.uv = float2(0, 1);
				output.Append(OUT);

				OUT.position = UnityObjectToClipPos(v0 + IN[0].normal * height - faceNormal * _GrassWidth/10);
				OUT.normal = float3(0,1,0);
				OUT.color = color;
				OUT.uv = float2(1, 0.7);
				output.Append(OUT);
				
				OUT.position = UnityObjectToClipPos(v0 + IN[0].normal * height*3/4 + perpendicular * _GrassWidth/3 - faceNormal * _GrassWidth/10);
				OUT.normal = float3(0,1,0);
				OUT.color = color;
				OUT.uv = float2(1, 1);
				output.Append(OUT);
				
				OUT.position = UnityObjectToClipPos(v0 + IN[0].normal * height);
				OUT.normal = faceNormal;
				OUT.color = color;
				OUT.uv = float2(0.5, 1);
				output.Append(OUT);

				OUT.position = UnityObjectToClipPos(v0 + IN[0].normal * height*3/4 + perpendicular * _GrassWidth/3);
				OUT.normal = faceNormal;
				OUT.color = color;
				OUT.uv = float2(1, 0.7);
				output.Append(OUT);
				*/
			}
			
			fixed4 frag (g2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col; // * float4(i.color.rgb,1);
			}
			ENDCG
		}
	}
}
