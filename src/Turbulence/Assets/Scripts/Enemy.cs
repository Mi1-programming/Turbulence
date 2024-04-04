using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatScoreEnemyTargetBias
{
    public string name;
    public float multiplier = 0.0f;
}
[System.Serializable]
public class EnemyTargetScorer
{
    public string name;
    public float multiplier = 1.0f;
    public float speed = 1.0f;
    public StatScoreEnemyTargetBias[] biases;
}
public class Enemy : MonoBehaviour
{
    [HideInInspector] public EnemyTarget target;
    [HideInInspector] public float speedDir;
    [SerializeField] private EnemyTargetScorer[] targetGroups;
    [SerializeField] private float targetRandomness;
    [SerializeField] private float targetRate = 1.0f;
    private Vector2 targetOffset;
    private Dictionary<string, EnemyTargetScorer> targetGroupsDict = new Dictionary<string, EnemyTargetScorer>();

    public virtual void FindTarget()
    {
        EnemyTarget bestTarget = null;
        float faults = float.MaxValue;
        foreach (var t in EnemyTarget.targets)
        {
            if (t.gameObject == gameObject) continue;

            EnemyTargetScorer scorer = targetGroupsDict[t.targetGroup];
            float fault = Vector2.Distance((Vector2)transform.position + targetOffset, t.transform.position) * scorer.multiplier;
            foreach (var bias in scorer.biases)
            {
                fault -= t.stats.statDictionary[bias.name].CurValue * bias.multiplier;
            }
            if (fault > faults) continue;
            faults = fault;
            bestTarget = t;
        }
        target = bestTarget;
        speedDir = targetGroupsDict[target.targetGroup].speed;
    }
    public virtual void Awake()
    {
        foreach (var group in targetGroups)
        {
            targetGroupsDict.Add(group.name, group);
        }
        targetOffset = Random.insideUnitCircle;
        InvokeRepeating("FindTarget", 0, 1.0f / targetRate);
    }
}
