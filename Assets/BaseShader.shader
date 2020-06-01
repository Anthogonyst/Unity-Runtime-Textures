// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BaseShader"
{
	Properties
	{
		_Pattern("Pattern", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Pattern;
		uniform float4 _Pattern_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color5 = IsGammaSpace() ? float4(0.3113208,0.219677,0.1982467,0) : float4(0.07896996,0.03957016,0.03256112,0);
			float4 color6 = IsGammaSpace() ? float4(0.509434,0.4012994,0.4012994,0) : float4(0.2228772,0.1337809,0.1337809,0);
			float2 uv_Pattern = i.uv_texcoord * _Pattern_ST.xy + _Pattern_ST.zw;
			float4 tex2DNode1 = tex2D( _Pattern, uv_Pattern );
			float4 lerpResult2 = lerp( color5 , color6 , tex2DNode1);
			o.Albedo = lerpResult2.rgb;
			float4 lerpResult7 = lerp( float4( 0,0,0,0 ) , ( float4( 0.6037736,0.6037736,0.6037736,0 ) * color6 ) , tex2DNode1);
			o.Emission = lerpResult7.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17000
7;1;1155;735;1184.019;669.3997;1.3;True;True
Node;AmplifyShaderEditor.ColorNode;6;-935,-268;Float;False;Constant;_Color1;Color 1;1;0;Create;True;0;0;False;0;0.509434,0.4012994,0.4012994,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-988,-100.7064;Float;True;Property;_Pattern;Pattern;0;0;Create;True;0;0;False;0;51ae79657bf97f5409712bacf54309dd;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-930,-434;Float;False;Constant;_Color0;Color 0;1;0;Create;True;0;0;False;0;0.3113208,0.219677,0.1982467,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-583.4201,-80.49963;Float;False;2;2;0;COLOR;0.6037736,0.6037736,0.6037736,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;9;-600.7195,6.200315;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;2;-576.835,-284.4958;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;7;-436.5215,-55.80022;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-187.8056,-285.47;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;BaseShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;8;1;6;0
WireConnection;9;0;1;0
WireConnection;2;0;5;0
WireConnection;2;1;6;0
WireConnection;2;2;1;0
WireConnection;7;1;8;0
WireConnection;7;2;9;0
WireConnection;0;0;2;0
WireConnection;0;2;7;0
ASEEND*/
//CHKSM=DE45A9CFBF34A9EEDFE5A66F18A5B0E6F260480B