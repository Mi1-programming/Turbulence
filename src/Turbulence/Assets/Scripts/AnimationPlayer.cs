using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    private Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();    
    }
    public void Play()
    {
        anim.Play();
    }
}
