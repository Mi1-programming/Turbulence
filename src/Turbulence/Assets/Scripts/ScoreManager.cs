using System;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private string addedString;
    public static ScoreManager instance;
    public UnityEvent<string> onNewScore;

    [HideInInspector] public int score;
    public int Score
    {
        get { return score; }
        set {
            score = value;
            onNewScore.Invoke($"{addedString}{score}");
        }
    }
    public void OnDisable()
    {
        int val = Convert.ToInt32(SaveSystem.GetValue("score"));
        if (score <= val) return;
        SaveSystem.SetValue("score", score);
    }
    public void UpdateScore(int newScore)
    {
        Score += newScore;
    }
    private void Awake()
    {
        instance = this;
    }
}
