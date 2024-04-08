using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingWeight : MonoBehaviour
{
    private PostProcessVolume post;

    private void Start()
    {
        post = GetComponent<PostProcessVolume>();
    }
    public void SetWeight(float value)
    {
        post.weight = value;
    }
}
