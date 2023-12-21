Shader "Custom/BaseColorMask" {
  SubShader {
    Tags {
      "Queue" = "Overlay-100"
      "RenderType" = "Overlay"
    }
    Pass {
      Name "Mask"
      Cull Off
      ZWrite Off
      ColorMask 0

      Stencil {
        Ref 1
        Pass Replace
      }
    }
  }
}