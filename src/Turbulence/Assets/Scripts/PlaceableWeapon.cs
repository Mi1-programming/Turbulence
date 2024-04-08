using UnityEngine;

[CreateAssetMenu(fileName = "PlaceableWeapon", menuName = "Weapon/PlaceableWeapon")]
public class PlaceableWeapon : Weapon
{
    [Header("Placeable Settings")]
    public GameObject Prefab;

    public override bool Use(Vector3 startPos, Vector3 dir)
    {
        Instantiate(Prefab, startPos, Quaternion.identity);
        return UseAmmo();
    }
}
