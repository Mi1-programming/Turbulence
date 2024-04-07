using UnityEngine;

public class GoToMouse : MonoBehaviour
{
    [SerializeField] private float smoothing = 1.0f;
    private Camera cam;
    private Vector3 vel;

    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        Vector3 newPos = cam.ScreenToWorldPoint((Vector2)Input.mousePosition);
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref vel, smoothing);
    }
}
