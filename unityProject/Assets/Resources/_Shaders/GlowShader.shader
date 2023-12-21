Shader "Custom/GlowShader"
{
    Properties
    {
        _Color ("Main Color", Color) = (.5,.5,.5,1)
        _GlowColor ("Glow Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _Glow ("Glow", Range (0, 1)) = 0.5
    }
    
    SubShader
    {
        Tags {"Queue" = "Overlay" }
        LOD 100
        
        CGPROGRAM
        #pragma surface surf Lambert
        
        struct Input
        {
            float2 uv_MainTex;
        };
        
        sampler2D _MainTex;
        
        fixed4 _Color;
        fixed4 _GlowColor;
        float _Glow;
        
        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
            
            // Add glow effect
            o.Emission = _Glow * _GlowColor;
        }
        ENDCG
    }
    Fallback "Diffuse"
}