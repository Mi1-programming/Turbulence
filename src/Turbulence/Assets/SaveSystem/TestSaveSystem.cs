using Newtonsoft.Json;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class TestSaveSystem : MonoBehaviour
{
    public string Key;

    [Header("Values")]
    public float FloatValue;
    public string StringValue;
    public bool BoolValue;
    public List<string> ListValue = new();

    [Header("Buttons")]
    public bool SetFloat;
    public bool SetString;
    public bool SetBool;
    public bool SetList;
    public bool Get;
    public bool ClearValues;

    [Header("Data show event")]
    public UnityEvent<string> ShowData;

    private void Start()
    {
        string screenDebug = "";
        object testdata = SaveSystem.GetValue("testdata");
        if (testdata != null) screenDebug += testdata.ToString();
        else screenDebug += "testdata = null";

        ShowData.Invoke(screenDebug + "\n" + SaveSystem.Log);
    }

    private void Update()
    {
        if (SetFloat)
        {
            SetFloat = false;
            SaveSystem.SetValue(Key, FloatValue);
        }
        if (SetString)
        {
            SetString = false;
            SaveSystem.SetValue(Key, StringValue);
        }
        if (SetBool)
        {
            SetBool = false;
            SaveSystem.SetValue(Key, BoolValue);
        }
        if (SetList)
        {
            SetList = false;
            SaveSystem.SetValue(Key, ListValue);
        }
        if (Get)
        {
            Get = false;
            object o = SaveSystem.GetValue(Key);
            FloatValue = o as float? ?? 0;

            StringValue = o as string;
            BoolValue = o as bool? ?? false;
            //ListValue = SaveSystem.GetValueAsList(Key, typeof(List<string>)) as List<string>;
        }
        if (ClearValues)
        {
            ClearValues = false;
            FloatValue = 0;
            StringValue = "";
            BoolValue = false;
            ListValue = new();
        }
    }

    public void AddTestData()
    {
        SaveSystem.SetValue("testdata", "testdata found! awesome");
        SaveSystem.Save();
        ShowData.Invoke(SaveSystem.Log);
    }
}
