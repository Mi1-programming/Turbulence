using UnityEngine;

public class DependStatOn : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Stats stats;
    [SerializeField] private string targetStat;
    [SerializeField] private float healAmount = 1.0f;
    [SerializeField] private float killAmount = 1.0f;
    bool isTargetInside = false;

    private void Update()
    {
        if (!isTargetInside)
        {
            stats.statDictionary[targetStat].CurValue -= killAmount * Time.deltaTime;
            return;
        }

        stats.statDictionary[targetStat].CurValue += healAmount * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target != collision.gameObject) return;
        isTargetInside = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (target != collision.gameObject) return;
        isTargetInside = false;
    }
}
