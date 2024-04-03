using UnityEngine;

[System.Serializable]
public struct ParallaxBlit
{
    public Texture texture;
    public float weight;
}
public class ParallaxBlitter : MonoBehaviour
{
    [SerializeField] private ParallaxBlit[] parallaxBlits; // The parallax background parts
    [SerializeField] private Material backgroundMaterial; // The material of the parallax
    [SerializeField] private Vector2 autoScrollSpeed; // Scroll automatically in a direction
    [SerializeField] private Transform scrollTarget; // The object that the parallax effect revolves around
    [SerializeField] private Vector2 targetScrollWeight; // The amount that moving the object moves the parallax
    private Material[] materials; // Store modified parallax materials

    private void Start()
    {
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
            Vector2 scroll = parallaxBlits[i].weight * (autoScrollSpeed * Time.time + (Vector2)scrollTarget.position * targetScrollWeight);
            materials[i].SetFloat("_ScrollX", scroll.x);
            materials[i].SetFloat("_ScrollY", scroll.y);
        }
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        for (int i = 0; i < parallaxBlits.Length; i++) { // Loop parallax effects
            Graphics.Blit(parallaxBlits[i].texture, destination, materials[i]); // Render parallax texture onto the screen, and scroll it using the shader
        }
    }
}
