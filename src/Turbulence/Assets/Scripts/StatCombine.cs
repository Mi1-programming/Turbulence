using UnityEngine;
using UnityEngine.Events;

public class StatCombine : MonoBehaviour
{
    [SerializeField] private string targetStat;
    [SerializeField] private LayerMask combineLayer;
    [SerializeField] private Stats stats;
    [SerializeField] private float gain = 0.5f;
    [SerializeField] private int combineChance = 4;
    [SerializeField] private UnityEvent onCombine;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!((combineLayer.value & (1 << collision.gameObject.layer)) > 0)) return;
        if (Random.Range(0, combineChance) != 0) return;

        Stat myStat = stats.statDictionary[targetStat];
        Stat otherStat = collision.gameObject.GetComponent<Stats>().statDictionary[targetStat];
        if (otherStat.CurValue >= myStat.CurValue) return;
        myStat.CurValue += otherStat.CurValue * gain;
        onCombine.Invoke();
        Destroy(collision.gameObject);
    }
}
