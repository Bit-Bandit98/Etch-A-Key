Shader "FillableRectangleOutline" {
    Properties {
        _Frac ("Fill Progress", Range(0,1)) = 1.0
        _FillColor ("Fill Color", Color) = (1,1,1,1)
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width", Range(0,0.1)) = 0.02
        [Toggle] _FlipU ("Flip Horizontal", Float) = 0.0
        [Toggle] _FlipV ("Flip Vertical", Float) = 0.0
    }

    SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "DisableBatching"="True"}

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            half _Frac;
            fixed4 _FillColor;
            fixed4 _OutlineColor;
            half _OutlineWidth;
            bool _FlipU;
            bool _FlipV;

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata_img v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                o.uv = v.texcoord.xy;

                if (_FlipU)
                    o.uv.x = 1.0 - o.uv.x;
                if (_FlipV)
                    o.uv.y = 1.0 - o.uv.y;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float2 uv = i.uv;
                
                // Define the outer and inner borders
                float borderOuterX = step(uv.x, _OutlineWidth) + step(1.0 - _OutlineWidth, uv.x);
                float borderOuterY = step(uv.y, _OutlineWidth) + step(1.0 - _OutlineWidth, uv.y);
                float borderInnerX = step(uv.x, _OutlineWidth + _Frac * (1.0 - 2.0 * _OutlineWidth)) - step(uv.x, _OutlineWidth);
                float borderInnerY = step(uv.y, _OutlineWidth + _Frac * (1.0 - 2.0 * _OutlineWidth)) - step(uv.y, _OutlineWidth);

                // Combine outer and inner borders to create the outline
                float border = max(borderOuterX, borderOuterY) - min(borderInnerX, borderInnerY);

                // Create the fill mask
                float fillMask = step(_OutlineWidth, uv.x) * step(uv.x, _OutlineWidth + _Frac * (1.0 - 2.0 * _OutlineWidth)) *
                                 step(_OutlineWidth, uv.y) * step(uv.y, _OutlineWidth + _Frac * (1.0 - 2.0 * _OutlineWidth));

                // Combine the fill mask and the outline
                float finalMask = max(fillMask, border);

                // Determine the color
                fixed4 color = lerp(_OutlineColor, _FillColor, fillMask);

                // Set alpha
                color.a *= finalMask;

                return color;
            }
            ENDCG
        }
    }
}