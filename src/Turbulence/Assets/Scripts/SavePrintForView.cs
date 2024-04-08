using UnityEngine;
using UnityEngine.Events;

public class SavePrintForView : MonoBehaviour
{
    [SerializeField] private UnityEvent<string> sentPrint;
    [SerializeField] private string data;
    [SerializeField] private string startingString;

    private void Start()
    {
        sentPrint.Invoke($"{startingString}{SaveSystem.GetValue(data)}");
    }
}
