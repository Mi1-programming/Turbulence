using UnityEngine;

public class Scaler : MonoBehaviour
{
    public void SetScale(float value)
    {
        transform.localScale = Vector3.one * value;
    }
}
