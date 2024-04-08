using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private string pauseKey;
    [SerializeField] private UnityEvent onPause;
    [SerializeField] private UnityEvent onResume;
    [SerializeField] private UnityEvent<bool> onIsPaused;
    [SerializeField] private UnityEvent<bool> onIsResumed;

    public static bool isPaused = false;

    public void SetState(bool pause)
    {
        Time.timeScale = pause ? 0.0f : 1.0f;
        if (pause) onPause.Invoke(); else onResume.Invoke();
        onIsPaused.Invoke(pause);
        onIsResumed.Invoke(!pause);
        isPaused = pause;
    }
    private void OnDisable()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
    }
    private void Update()
    {
        if (!Input.GetButtonDown(pauseKey)) return;
        SetState(!isPaused);
    }
}
