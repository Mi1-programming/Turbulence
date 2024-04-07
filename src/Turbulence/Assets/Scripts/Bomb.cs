using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Object prefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
