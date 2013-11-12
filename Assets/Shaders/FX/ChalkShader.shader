Shader "Custom/FX/Chalk"{

	Properties{

		_Color ("Main Color", Color) = (0, 0, 0, 0)
		_MainTex("Base (RGB)", 2D) = "white"{}
		_DecalTex("Decal (RGBA)", 2D) = "black"{}
	}

	SubShader{

		Tags{

			"RenderType" = "Opaque"
		}

		LOD 200


		CGPROGRAM
		#pragma surface surf Lambert

			fixed4 _Color;
			sampler2D _MainTex;
			sampler2D _DecalTex;

			struct Input{

				float2 uv_MainTex;
				float2 uv_DecalTex;
			};

			void surf(Input IN, inout SurfaceOutput o){

				fixed4  k       = _Color;
				half4   c       = tex2D(_MainTex, IN.uv_MainTex);
				half4   decal   = tex2D(_DecalTex, IN.uv_DecalTex);


				k = 1 - _Color;

				c.rgb *= k;

				decal.rgb = 1 - decal.rgb;
				decal.rgb *= k;

				c.rgb = lerp(c.rgb, decal.rgb, decal.a);

				o.Albedo = 1 - c.rgb;
				o.Alpha = c.a;
			}

		ENDCG
	}

	FallBack "Mobile/Diffuse"
}
