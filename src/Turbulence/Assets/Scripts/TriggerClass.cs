using UnityEngine;
using UnityEngine.Events;

public class TriggerClass : MonoBehaviour
{
    [SerializeField] private UnityEvent onEnter;
    [SerializeField] private UnityEvent onExit;
    [SerializeField] private bool destroyTarget = false;

    private void Logic(Collider2D collision)
    {
        if (destroyTarget) Destroy(collision.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onEnter.Invoke(); // Triggers event on 2d trigger enter
        Logic(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onExit.Invoke(); // Triggers event on 2d trigger exit
        Logic(collision);
    }
}
