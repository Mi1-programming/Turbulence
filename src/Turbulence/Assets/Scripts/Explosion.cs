using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float valueChange;
    [SerializeField] private string stat;
    [SerializeField] private float knockBack = 5.0f;
    [SerializeField] private float destroyAfter = 1.0f;

    private void Start()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var c in colliders)
        {
            Stats stats = null;
            c.TryGetComponent<Stats>(out stats);
            if (stats == null) continue;
            if (!stats.statDictionary.ContainsKey(stat)) return;

            Vector2 point = c.ClosestPoint(transform.position);
            float distVal = Mathf.Max(radius - Vector2.Distance(transform.position, point), 0);
            stats.statDictionary[stat].CurValue += distVal * valueChange;

            Rigidbody2D rb2d = null;
            c.TryGetComponent<Rigidbody2D>(out rb2d);
            if (rb2d == null) continue;

            Vector2 force = knockBack * Vector3.Normalize(point - (Vector2)transform.position) * distVal;
            rb2d.AddForce(force, ForceMode2D.Impulse);
        }

        Destroy(gameObject, destroyAfter);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
