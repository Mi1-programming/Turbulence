using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon Weapon;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SetWeapon(Weapon);
    }

    private void Update()
    {
        if (Weapon == null) return;

        Weapon.DecreaseCooldown();
        if (Input.GetMouseButton(0))
        {
            if (Weapon.Use(transform.position, transform.right))
            {
                RemoveWeapon();
            }
        }
    }


    public void SetWeapon(Weapon weapon)
    {
        Weapon = weapon;
        Weapon.Setup();
        sr.sprite = weapon.Sprite;
        transform.localPosition = transform.right * Weapon.HoldDistance;
        transform.localScale = Vector3.one * Weapon.WeaponScale;
    }

    public void RemoveWeapon()
    {
        Weapon = null;
        sr.sprite = null;
    }
}
