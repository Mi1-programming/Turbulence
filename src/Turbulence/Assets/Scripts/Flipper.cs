using UnityEngine;

public class Flipper : MonoBehaviour
{
    private Vector3 currentScale;

    private void Start()
    {
        currentScale = transform.localScale;
    }
    private void Update()
    {
        Vector3 newScale = currentScale;
        newScale.y = transform.right.x < 0 ? -newScale.y : newScale.y;
        transform.localScale = newScale;
    }
}
