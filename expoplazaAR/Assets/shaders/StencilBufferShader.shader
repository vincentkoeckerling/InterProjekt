Shader "Custom/StencilBufferShader"
{
    Properties
    {
    }
    SubShader
    {
        Tags { "Queue" = "Geometry"}

        ColorMask 0 //object doesnt Render itself, cause its only a stencil
        ZWrite Off
        
        Stencil{
            Ref 1
            Comp always
            Pass replace
            }
        
        CGPROGRAM

        #pragma surface surf Lambert

        struct Input
        {
            float3 worldPos;//Because empty Input makes an error
        };


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = fixed4(1,1,1,1);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
