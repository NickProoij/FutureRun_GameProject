Shader "UI/RoundedCorners"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Radius("Radius", Range(0, 1)) = 0.5
    }
        SubShader
        {
            Tags { "Queue" = "Overlay" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                };

                struct v2f
                {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float _Radius;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.texcoord);

                    float2 pos = i.texcoord * 2.0 - 1.0;
                    float dist = length(pos);
                    col.a *= smoothstep(0.5, 0.5 - fwidth(dist), 0.5 - _Radius + dist);

                    return col;
                }
                ENDCG
            }
        }
}