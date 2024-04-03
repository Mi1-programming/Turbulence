using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private string horizontal;
    [SerializeField] private string jump;
    [SerializeField] private float turnSpeed = 10.0f;
    [SerializeField] private float jumpSpeed = 10.0f;
    [SerializeField] private float jumpCooldown = 0.5f;
    private float jumpWaited = 0.0f;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float h = Input.GetAxis(horizontal);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, - h * turnSpeed + transform.eulerAngles.z);
    }
    private void Update()
    {
        jumpWaited += Time.deltaTime;
        if (jumpWaited < jumpCooldown) return;
        if (!Input.GetButtonDown(jump)) return;
        jumpWaited = 0.0f;
        rb2d.AddRelativeForce(jumpSpeed * Vector2.right, ForceMode2D.Impulse);
    }
}
