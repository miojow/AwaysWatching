Shader "Custom/CustomShader" {
	 Properties {
      _MainTex ("Base (RGB)", 2D) = "white" {}
   }
   SubShader {
      Tags { "RenderType"="Opaque" }
      LOD 200
      
      CGPROGRAM
      #pragma surface surf Lambert

      sampler2D _MainTex;

      struct Input {
         float2 uv_MainTex;
      };

      void surf (Input IN, inout SurfaceOutput o) {
         half4 c = tex2D (_MainTex, IN.uv_MainTex);
         half4 f = c;
         f.r = (c.r + c.g + c.b) / 3;
         f.g = f.r;
         f.b = f.r;
         c = f;
         o.Albedo = (c.g + c.b + (c.g/2));
         o.Alpha = c.a;
      }
      ENDCG
   } 
   FallBack "Diffuse"
}
