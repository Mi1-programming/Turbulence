using UnityEngine;
using UnityEngine.Events;

public class DependStatOn : MonoBehaviour
{
    [SerializeField] private string target;
    [SerializeField] private Stats stats;
    [SerializeField] private string targetStat;
    [SerializeField] private float healAmount = 1.0f;
    [SerializeField] private float killAmount = 1.0f;
    [SerializeField] private UnityEvent onEnter;
    [SerializeField] private UnityEvent onExit;
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
    private bool ValidTarget(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(target)) return false;
        return true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ValidTarget(collision)) return;

        isTargetInside = true;
        onEnter.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ValidTarget(collision)) return;

        isTargetInside = false;
        onExit.Invoke();
    }
}
