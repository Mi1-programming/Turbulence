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
        if (Input.GetMouseButton(0))
        {
            Weapon.Use(transform.position, transform.right);
        }
        Weapon.DecreaseCooldown();
    }


    public void SetWeapon(Weapon weapon)
    {
        Weapon = weapon;
        sr.sprite = weapon.Sprite;
        transform.localPosition = transform.right * Weapon.HoldDistance;
    }
}
