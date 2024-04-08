using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private UnityEvent<string> onAmmoChange;
    [SerializeField] private UnityEvent onUse;
    public Weapon weapon;
    public Weapon Weapon
    {
        get { return weapon; }
        set {
            weapon = value;
            if (weapon == null)
            {
                sr.sprite = null;
                return;
            }
            weapon.Setup();
            sr.sprite = weapon.sprite;
        }
    }
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Weapon = weapon;
    }
    private void Update()
    {
        if (Weapon == null)
        {
            onAmmoChange.Invoke("");
            return;
        }
        onAmmoChange.Invoke($"{Weapon.ammo} / {Weapon.magazineSize}");

        Weapon.DecreaseCooldown();

        if (!Input.GetMouseButton(0)) return;

        if (Weapon.Cooldown > 0) return;
        Weapon.Cooldown = 1 / Weapon.firerate;

        onUse.Invoke();
        if (Weapon.Use(transform.position, transform.right))
        {
            Weapon = null;
        }
    }
}
