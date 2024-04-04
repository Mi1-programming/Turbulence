using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public Stats stats;
    public string targetGroup;
    public static List<EnemyTarget> targets = new List<EnemyTarget>();

    private void OnEnable()
    {
        targets.Add(this);
    }
    private void OnDisable()
    {
        targets.Remove(this);
    }
}
