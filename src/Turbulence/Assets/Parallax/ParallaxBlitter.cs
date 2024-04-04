using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct ParallaxTarget
{
    public string name;
    public Transform follow;
}
[System.Serializable]
public struct ParallaxBlit
{
    public Texture texture;
    public string target;
    public Vector2 weights;
    public Vector2 scroll;
    [HideInInspector] public Vector2 pos;
}
public class ParallaxBlitter : MonoBehaviour
{
    [SerializeField] private ParallaxTarget[] targets;
    [SerializeField] private ParallaxBlit[] parallaxBlits; // The parallax background parts
    [SerializeField] private Material backgroundMaterial; // The material of the parallax

    private Dictionary<string, Transform> targetDict = new Dictionary<string, Transform>();
    private Material[] materials; // Store modified parallax materials

    private void Start()
    {
        foreach (var target in targets) targetDict.Add(target.name, target.follow);

        materials = new Material[parallaxBlits.Length]; // Create materials for adjusting their properties
        for (int i = 0; i < parallaxBlits.Length; i++)
        { // Loop parallaxes
            materials[i] = Instantiate(backgroundMaterial); // Create new instance of the backgrond material
            materials[i].SetTexture("_MainTex", parallaxBlits[i].texture); // Sets the main texture of the material
        }
    }
    private void Update()
    {
        for (int i = 0; i < parallaxBlits.Length; i++)
        { // Loop parallaxes
            // Update the offset variables
            ParallaxBlit pBlit = parallaxBlits[i];
            pBlit.pos = ((pBlit.target != string.Empty) ? (Vector2)targetDict[pBlit.target].position * pBlit.weights : Vector2.zero) + Time.time * pBlit.scroll;
            materials[i].SetFloat("_ScrollX", pBlit.pos.x);
            materials[i].SetFloat("_ScrollY", pBlit.pos.y);
        }
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        for (int i = 0; i < parallaxBlits.Length; i++) { // Loop parallax effects
            Graphics.Blit(parallaxBlits[i].texture, destination, materials[i]); // Render parallax texture onto the screen, and scroll it using the shader
        }
    }
}
