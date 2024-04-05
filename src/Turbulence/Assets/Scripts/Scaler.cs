using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private Vector2 scaleRange;

    public void SetScale(float value)
    {
        transform.localScale = Vector3.one * Mathf.LerpUnclamped(scaleRange.x, scaleRange.y, value);
    }
}
