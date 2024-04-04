using UnityEngine;

public class Flipper : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        sprite.flipY = transform.right.x < 0;
    }
}
