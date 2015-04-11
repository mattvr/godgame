Shader "Custom/TransMask" 
{
	Properties 
	{
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_DissolveTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_DissolveAmount ("Dissolve", Range(0.0, 1.0)) = 1.0
		_Color ("Color", COLOR) = (1, 1, 1, 1)
	}
	
	SubShader
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		ZTest Always ZWrite Off Lighting Off Cull Off Fog { Mode Off } Blend SrcAlpha OneMinusSrcAlpha
		LOD 110
		
		Pass 
		{
			CGPROGRAM
			#pragma vertex vert_vct
			#pragma fragment frag_mult 
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _DissolveTex;
			float4 _MainTex_ST;
			float4 _Color;
			float _DissolveAmount;

			struct vin_vct 
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f_vct
			{
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			v2f_vct vert_vct(vin_vct v)
			{
				v2f_vct o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = v.texcoord;
				return o;
			}

			fixed4 frag_mult(v2f_vct i) : COLOR
			{
				fixed4 col = tex2D(_MainTex, i.texcoord) * i.color * _Color;
//				clip(tex2D(_DissolveTex, i.texcoord) - _DissolveAmount);
				fixed4 diss = tex2D(_DissolveTex, i.texcoord);
				if (diss.r <= _DissolveAmount) {
					discard;
				}
				
				return col;
			}
			
			ENDCG
		} 
	}
}
