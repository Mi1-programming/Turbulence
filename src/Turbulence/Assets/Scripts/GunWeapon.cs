using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunWeapon", menuName = "Weapon/GunWeapon")]
public class GunWeapon : Weapon
{
    [Header("Gun Settings")]
    public Sprite BulletSprite;
    public int BulletLayer = 8;
    public float BulletStartDistance;
    public float BulletScale;
    public int BulletAmount;
    public float BulletSpeed;
    public float BulletDamage;
    public float Firerate;

    private int bullets;

    public override void Setup()
    {
        bullets = BulletAmount;
    }

    public override bool Use(Vector3 startPos, Vector3 dir)
    {
        if (Cooldown > 0) return false;
        Cooldown = 1 / Firerate;

        Debug.Log($"weapon '{name}' used.        (startPos: {startPos})        (targetPos: {dir})");

        GameObject _obj = new GameObject($"{Name} Bullet");
        SpriteRenderer _sr = _obj.AddComponent<SpriteRenderer>();
        CircleCollider2D _col = _obj.AddComponent<CircleCollider2D>();
        Rigidbody2D _rb = _obj.AddComponent<Rigidbody2D>();
        Bullet _bullet = _obj.AddComponent<Bullet>();

        _bullet.Damage = BulletDamage;

        _sr.sprite = BulletSprite;
        _obj.layer = BulletLayer;
        _obj.transform.position = startPos + dir * BulletStartDistance;
        _obj.transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
        _obj.transform.localScale = Vector3.one * BulletScale;

        _rb.gravityScale = 0f;
        _rb.velocity = dir * BulletSpeed;

        Destroy(_obj, 5f);

        // Bullet count
        bullets--;
        if (bullets <= 0)
        {
            return true;
        }
        return false;
    }
}
