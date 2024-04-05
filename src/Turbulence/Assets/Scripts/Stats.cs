using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

[System.Serializable]
public class Stat
{
    // Main properties
    public string name;
    public float startingValue = 1f;
    public float curValue = 1f;
    public bool hasFloor = true;
    public bool hasCeiling = true;
    public float startingRandomness = 0.5f;

    // EVENTS
    public UnityEvent<float> onStatUpdate;
    public UnityEvent<float> onStatUpdateInverse;
    public UnityEvent onStatIncrease;
    public UnityEvent onStatDecrease;
    public UnityEvent onStatEnd;

    public float CurValue
    {
        get { return curValue; }
        set
        {
            if (hasFloor) value = Mathf.Max(0, value);
            if (hasCeiling) value = Mathf.Min(value, startingValue);

            if (curValue == value) return; // Return if the value wasn't actually changed

            float normVal = value / startingValue; // Create variable with the value normalized to the range of 0 to 1

            // Invoke events
            onStatUpdate.Invoke(normVal); // Send the normalized value to event
            onStatUpdateInverse.Invoke(1.0f - normVal); // Invert the normalized value to event

            // Differentiate between adding and taking away value as events
            if (curValue > value) onStatDecrease.Invoke();
            if (curValue < value) onStatIncrease.Invoke();

            if (value == 0) onStatEnd.Invoke(); // If the value has reached 0 send to event

            curValue = value; // Finally set the actual value
        }
    }
}
public class Stats : MonoBehaviour
{
    [SerializeField] private Stat[] stats; // List of stats which will be in a dictionary later
    [HideInInspector] public Dictionary<string, Stat> statDictionary; // List of stats which can be accessed from other components

    private void Start()
    {
        // Set starting values
        foreach (var hs in stats)
        {
            hs.CurValue = hs.startingValue - Random.value * hs.startingRandomness;
        }   
    }
    private void Awake()
    {
        // Creates a dictionary version
        statDictionary = new Dictionary<string, Stat>();
        foreach (var hs in stats)
        {
            statDictionary.Add(hs.name, hs);
        }
    }
}