using UnityEngine;

public class PhysicsEnemy : Enemy
{
    [SerializeField] private float speed;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (target == null) return;

        Vector2 dir = Vector3.Normalize(target.transform.position - transform.position) * speedDir;
        rb2d.AddForce(speed * dir);
    }
}
