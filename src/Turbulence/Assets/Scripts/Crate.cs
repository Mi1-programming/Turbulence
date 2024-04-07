using UnityEngine;
using UnityEngine.Events;

public class Crate : MonoBehaviour
{
    [SerializeField] private UnityEvent onCollect;
    [SerializeField] private Weapon[] drops;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WeaponManager wm = collision.GetComponentInChildren<WeaponManager>();
        if (wm == null) return;

        wm.Weapon = drops[Random.Range(0, drops.Length)];

        onCollect.Invoke();
        Destroy(gameObject);
    }
}
