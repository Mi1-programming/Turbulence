using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    [Header("Normal Settings")]
    public string Name;
    public Sprite Sprite;
    public float HoldDistance;

    [HideInInspector] public float Cooldown;

    public abstract void Use(Vector3 startPos, Vector3 dir);

    // REMEMBER TO CALL THIS FROM WEAPONMANAGER
    public void DecreaseCooldown()
    {
        if (Cooldown > 0f) Cooldown -= Time.deltaTime;
    }
}
