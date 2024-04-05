using UnityEngine;

public class Instancer : MonoBehaviour
{
    [SerializeField] private bool destroyOnInstance;
    [SerializeField] private float destroyInstancedAfter = 1.0f;
    [SerializeField] private Transform overridePosition;

    public void Instance(Object target)
    {
        Transform instanceTo = (overridePosition != null) ? overridePosition : transform;
        if (target != null) Destroy(Instantiate(target, instanceTo.position, instanceTo.rotation) as GameObject, destroyInstancedAfter);
        if (destroyOnInstance) Destroy(gameObject);
    }
}
