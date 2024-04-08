using System;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    [Header("Normal Settings")]
    public string Name;
    public Sprite sprite;
    public int magazineSize;
    public float firerate;
    [NonSerialized] public int ammo;

    [HideInInspector] public float Cooldown;

    public virtual bool UseAmmo()
    {
        // Bullet count
        ammo--;
        if (ammo <= 0)
        {
            return true;
        }
        return false;
    }
    public virtual void Setup()
    {
        ammo = magazineSize;
    }// CALL WHEN APPLYING THE WEAPON IN WEAPONMANAGER

    public abstract bool Use(Vector3 startPos, Vector3 dir); // BOOL RETURN -> FALSE: did not break after use | TRUE: broke after use


    // REMEMBER TO CALL THIS FROM WEAPONMANAGER
    public void DecreaseCooldown()
    {
        if (Cooldown > 0f) Cooldown -= Time.deltaTime;
    }
}
