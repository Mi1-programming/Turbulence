using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    private Transform cam;
    private Vector3 vel;

    private void Start()
    {
        cam = Camera.main.transform;
    }
    private void LateUpdate()
    {
        Vector3 newPos = Vector3.SmoothDamp(cam.position, target.position, ref vel, speed);
        cam.position = new Vector3(newPos.x, newPos.y, cam.position.z);
    }
}
