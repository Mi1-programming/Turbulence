Shader "Hidden/RepeatingTextureThreshold"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TexParm1("TexParm1", 2D) = "white" {}
        _Prog ("Transition Progress", Float) = 0.5
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _Prog;
            sampler2D _TexParm1;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 parm1 = tex2D(_TexParm1, (i.uv * 8) % 1);
                float light = (parm1.r + parm1.g + parm1.b) / 4;
                col.rgb = (light > _Prog) ? col.rgb : float3(0, 0, 0);
                return col;
            }
            ENDCG
        }
    }
}
