using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunWeapon", menuName = "Weapon/GunWeapon")]
public class GunWeapon : Weapon
{
    [Header("Gun Settings")]
    public int Bullets;
    public float BulletSpeed;
    public float BulletDamage;
    public float BulletStartDistance;
    public float Firerate;

    public override void Use(Vector3 startPos, Vector3 dir)
    {
        if (Cooldown > 0) return;
        Cooldown = 1 / Firerate;

        Debug.Log($"weapon '{name}' used.        (startPos: {startPos})        (targetPos: {dir})");

        GameObject _obj = new GameObject($"{Name} Bullet");
        SpriteRenderer _sr = _obj.AddComponent<SpriteRenderer>();
        CircleCollider2D _col = _obj.AddComponent<CircleCollider2D>();
        Rigidbody2D _rb = _obj.AddComponent<Rigidbody2D>();

        _sr.sprite = Sprite;
        _obj.transform.position = startPos + dir * BulletStartDistance;
        _obj.transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);

        _rb.gravityScale = 0f;
        _rb.velocity = dir * BulletSpeed;

        Destroy(_obj, 5f);
    }
}
