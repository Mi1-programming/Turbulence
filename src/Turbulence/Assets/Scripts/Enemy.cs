using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum StatScoreCondition { ALWAYS, MORE, LESS };
[System.Serializable]
public class StatScoreEnemyTargetBias
{
    public string name;
    public float multiplier = 0.0f;
    public StatScoreCondition condition;
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
    [HideInInspector] public EnemyTargetScorer targetGroup;

    [SerializeField] private Stats stats;
    [SerializeField] private EnemyTargetScorer[] targetGroups;
    [SerializeField] private float targetRandomness;
    [SerializeField] private float targetRate = 1.0f;

    private Vector2 targetOffset;

    public float CompareStat(StatScoreCondition condition, Stat cur, Stat self)
    {
        switch (condition)
        {
            case StatScoreCondition.ALWAYS:
                return cur.CurValue;
            case StatScoreCondition.MORE:
                return Mathf.Max(0, self.CurValue - cur.CurValue);
            case StatScoreCondition.LESS:
                return Mathf.Max(0, cur.CurValue - self.CurValue);
        }
        return 0;
    }
    public virtual void FindTarget()
    {
        EnemyTarget bestTarget = null;
        EnemyTargetScorer bestGroup = null;
        float bestScore = float.MinValue;
        
        foreach (var t in EnemyTarget.targets)
        {
            if (t.gameObject == gameObject) continue;
            foreach (var group in targetGroups)
            {
                if (group.name != t.targetGroup) continue;

                float score = Vector2.Distance((Vector2)transform.position + targetOffset, t.transform.position) * -group.multiplier;
                foreach (var bias in group.biases)
                {
                    Stat cur = t.stats.statDictionary[bias.name];
                    Stat self = stats.statDictionary[bias.name];
                    float result = CompareStat(bias.condition, cur, self);
                    score += result * bias.multiplier;
                }
                if (score < bestScore) continue;

                bestTarget = t;
                bestGroup = group;
                bestScore = score;
            }
        }
        target = bestTarget;
        targetGroup = bestGroup;
    }
    public virtual void Awake()
    {
        targetOffset = Random.insideUnitCircle;
        InvokeRepeating("FindTarget", 0, 1.0f / targetRate);
    }
}
