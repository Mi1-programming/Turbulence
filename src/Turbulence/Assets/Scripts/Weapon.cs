using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    [Header("Normal Settings")]
    public string Name;
    public Sprite Sprite;
    public float WeaponScale;
    public float HoldDistance;

    [HideInInspector] public float Cooldown;

    public abstract void Setup(); // CALL WHEN APPLYING THE WEAPON IN WEAPONMANAGER

    public abstract bool Use(Vector3 startPos, Vector3 dir); // BOOL RETURN -> FALSE: did not break after use | TRUE: broke after use

    

    // REMEMBER TO CALL THIS FROM WEAPONMANAGER
    public void DecreaseCooldown()
    {
        if (Cooldown > 0f) Cooldown -= Time.deltaTime;
    }
}
