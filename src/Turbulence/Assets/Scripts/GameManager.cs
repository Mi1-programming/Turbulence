using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEvent onBeginGame;
    [SerializeField] private UnityEvent onGameLose;
    [SerializeField] private float loseDelay = 1.0f;
    public static GameManager instance;
    public GameObject player;
    public UnityAction onGameBegin;

    public void BeginGame()
    {
        onBeginGame.Invoke();
        if (onGameBegin != null) onGameBegin.Invoke();
    }
    private IEnumerator ILoseGame()
    {
        yield return new WaitForSeconds(loseDelay);
        onGameLose.Invoke();
    }
    public void LoseGame()
    {
        StartCoroutine(ILoseGame());
    }
    private void Awake()
    {
        instance = this;
    }
}
