using UnityEngine;
using UnityEngine.Events;

public class Despawner : MonoBehaviour
{
    [SerializeField] private float despawnTime = 10.0f;
    [SerializeField] private UnityEvent onDespawn;
    [SerializeField] private float delay = 0.0f;

    private void Despawn()
    {
        onDespawn.Invoke();   
        Destroy(gameObject, delay);
    }
    private void Start()
    {
        Invoke("Despawn", despawnTime);
    }
}
