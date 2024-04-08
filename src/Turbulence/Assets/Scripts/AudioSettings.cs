using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class AudioChannelSetting
{
    public string name;
    public AudioMixer mixer;
    public Slider adjuster;
    public string volumePar;
}
public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioChannelSetting[] settings;
    [SerializeField] private Vector2 volumeRange;

    private void ApplyAdjuster(AudioChannelSetting setting)
    {
        setting.mixer.SetFloat(setting.volumePar, Mathf.Lerp(volumeRange.x, volumeRange.y, setting.adjuster.value));
        SaveSystem.SetValue(setting.name, (int)(setting.adjuster.value * 100));
    }
    private void Start()
    {
        foreach (var setting in settings)
        {
            float loadedValue = (float)Convert.ToInt32(SaveSystem.GetValue(setting.name)) / 100;
            setting.adjuster.value = loadedValue;
            ApplyAdjuster(setting);
            setting.adjuster.onValueChanged.AddListener(delegate
            {
                ApplyAdjuster(setting);
            });
        }
    }
}
