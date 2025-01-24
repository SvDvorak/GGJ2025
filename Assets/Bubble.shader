Shader "GGJ/BubbleShader"
{
    Properties
    {
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
        Cull Front
        ZTest Always
        ZWrite Off
        
        Stencil
        {
            Ref 8
            WriteMask 8
            Comp Never
            Fail Replace
            Pass Replace
            ZFail Replace
            
        }

        Pass { }
    }
}
