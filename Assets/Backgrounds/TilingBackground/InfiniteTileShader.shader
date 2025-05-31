Shader "Custom/TilingSpriteShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TileX ("Tile X", Float) = 1.0
        _TileY ("Tile Y", Float) = 1.0
        _VariationIntensity ("Variazione Intensità", Range(0.001, 0.05)) = 0.01
        _ColorVariation ("Variazione Colore", Range(0.001, 0.1)) = 0.02
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent" "RenderType"="Transparent"
        }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float _TileX;
            float _TileY;
            float _VariationIntensity;
            float _ColorVariation;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float variation : TEXCOORD1;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                float2 tileIndex = floor(v.uv * float2(_TileX, _TileY));
                // Usa funzioni diverse per introdurre variazione
                float offsetX = sin(tileIndex.x * 0.741 + tileIndex.y * 0.237) * _VariationIntensity;
                float offsetY = cos(tileIndex.x * 0.841 + tileIndex.y * 0.637) * _VariationIntensity;

                // Memorizza un valore di variazione per il colore
                o.variation = (sin(tileIndex.x * 0.437 + tileIndex.y * 0.531) +
                    cos(tileIndex.y * 0.327 - tileIndex.x * 0.238) * 0.5 +
                    sin((tileIndex.x + tileIndex.y) * 0.189) * 0.3) * _ColorVariation * 0.7;

                // Applica piccole variazioni alle coordinate UV
                o.uv = v.uv * float2(_TileX, _TileY) + float2(offsetX, offsetY);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, frac(i.uv));
                // Applica sottili variazioni di luminosità
               col.rgb = lerp(col.rgb, col.rgb * (1.0 + i.variation), 0.8);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Sprites/Default"
}