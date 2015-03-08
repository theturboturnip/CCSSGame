Shader "Custom/Unlit Single Color" {
    Properties {
       	_Color ("Main Color", Color) = (1,1,1,1)

    }
    Category {
       	Lighting Off
       	ZWrite On
       	Cull Back
       	SubShader {
    		Color [_Color]
    		Pass{
    		SetTexture[_] {
                constantColor [_Color]
                Combine constant
            }
            }
   		}
    }
}