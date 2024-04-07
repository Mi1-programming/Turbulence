using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public float damage;
    [HideInInspector] public float knockback;
    [SerializeField] private UnityEvent onHit;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb2d.MovePosition(transform.position + transform.right * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Stats stats = null;
        collision.TryGetComponent<Stats>(out stats);
        if (stats != null)
        {
            if (stats.statDictionary.ContainsKey("hp")) stats.statDictionary["hp"].CurValue -= damage;
        }
        if (collision.attachedRigidbody != null)
        {
            collision.attachedRigidbody.AddForce(transform.right * knockback, ForceMode2D.Impulse);
        }
        onHit.Invoke();
        Destroy(gameObject);
    }
}
