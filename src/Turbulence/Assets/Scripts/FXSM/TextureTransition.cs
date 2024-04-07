using UnityEngine;

[CreateAssetMenu(fileName = "Texture Transition", menuName = "Transitions/Texture Transition")]
public class TextureTransition : Transition
{
    public Texture2D textureParm;

    public override void SetShaderVariables(RenderTexture source, RenderTexture destination, Material mat)
    {
        base.SetShaderVariables(source, destination, mat);
        mat.SetTexture("_TexParm1", textureParm);
    }
}
