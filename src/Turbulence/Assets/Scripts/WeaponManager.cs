using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon weapon;
    public Weapon Weapon
    {
        get { return weapon; }
        set {
            weapon = value;
            if (weapon == null)
            {
                Weapon = null;
                sr.sprite = null;
                return;
            }
            weapon.Setup();
            sr.sprite = weapon.Sprite;
            transform.localPosition = Vector2.right * weapon.HoldDistance;
        }
    }
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Weapon == null) return;

        Weapon.DecreaseCooldown();
        if (Input.GetMouseButton(0))
        {
            if (Weapon.Use(transform.position, transform.right))
            {
                Weapon = null;
            }
        }
    }
}
