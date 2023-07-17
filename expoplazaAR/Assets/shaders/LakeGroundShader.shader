Shader "Custom/LakeGroundShader"
{
    Properties
    {
    }
    SubShader
    {
        Tags { "Queue" = "Geometry" }
        
        Stencil{
            Ref 1
            Comp equal
            Pass keep
            }

CGPROGRAM
        
        #pragma surface surf Lambert

        struct Input
        {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
           
        }
        ENDCG
    }
    FallBack "Diffuse"
}
