using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GunWeapon", menuName = "Weapon/GunWeapon")]
public class GunWeapon : Weapon
{
    [Header("Gun Settings")]
    public UnityEngine.Object bullet;
    public float bulletStartDistance;
    public float bulletSpeed;
    public float bulletDamage;
    public float firerate;
    public float knockback;
    public int magazineSize;
    [NonSerialized] private int ammo;

    public override void Setup()
    {
        ammo = magazineSize;
    }
    public override bool Use(Vector3 startPos, Vector3 dir)
    {
        if (Cooldown > 0) return false;
        Cooldown = 1 / firerate;

        GameObject newBullet = Instantiate(bullet, startPos + dir * bulletStartDistance, Quaternion.FromToRotation(Vector3.right, dir)) as GameObject;
        newBullet.name = $"{Name} Bullet";
        Bullet bulletScript = newBullet.GetComponent<Bullet>();

        bulletScript.damage = bulletDamage;
        bulletScript.speed = bulletSpeed;
        bulletScript.knockback = knockback;

        // Bullet count
        ammo--;
        if (ammo <= 0)
        {
            return true;
        }
        return false;
    }
}
