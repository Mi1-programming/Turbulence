using UnityEngine;

[CreateAssetMenu(fileName = "GunWeapon", menuName = "Weapon/GunWeapon")]
public class GunWeapon : Weapon
{
    [Header("Gun Settings")]
    public Object bullet;
    public float bulletStartDistance;
    public float bulletSpeed;
    public float bulletDamage;
    public float knockback;

    public override bool Use(Vector3 startPos, Vector3 dir)
    {
        GameObject newBullet = Instantiate(bullet, startPos + dir * bulletStartDistance, Quaternion.FromToRotation(Vector3.right, dir)) as GameObject;
        newBullet.name = $"{Name} Bullet";
        Bullet bulletScript = newBullet.GetComponent<Bullet>();

        bulletScript.damage = bulletDamage;
        bulletScript.speed = bulletSpeed;
        bulletScript.knockback = knockback;

        return UseAmmo();
    }
}
