using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;


public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    public static string Log; // Currently gets no logs, because code is gguuud and works

    // Inspector Fields, bools, buttons
    [SerializeField] private List<string> DataDebug = new();
    [SerializeField] private bool DisableCrypting = false;
    [SerializeField][Header("Buttons")] private bool ClearData;
    [SerializeField] private bool PrintRandomCryptingKeys;
    [SerializeField] private bool OpenDataFolder;

    // Private static data
    private static Dictionary<string, object> data = new();
    private static string path;
    private static char sep;
    private static string key = "XUmn6lDv3jZwB3N74VGOWGfjkXLuTqga"; // 32chr crypting key
    private static string iv = "mCszBT819evHKmDm"; // 16chr crypting key


    #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void UpdateFileSystem();    
    #endif

    private void Awake()
    {
        Instance = this;
        path = Application.persistentDataPath;
        sep = Path.DirectorySeparatorChar;

        Load();
        UpdateDebug();
    }

    private void OnDisable()
    {
        Save();
    }



    #region Debug

    public void UpdateDebug()
    {
#if UNITY_EDITOR
        DataDebug.Clear();
        foreach (string _key in data.Keys)
        {
            DataDebug.Add($"{_key}: {data[_key]}");
        }
#endif
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (PrintRandomCryptingKeys)
        {
            PrintRandomCryptingKeys = false;

            string _chrs = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string _key = ""; // 32 chrs
            for (int i = 0; i < 32; i++)
            {
                _key += _chrs[UnityEngine.Random.Range(0, _chrs.Length)];
            }
            string _iv = "";
            for (int i = 0; i < 16; i++)
            {
                _iv += _chrs[UnityEngine.Random.Range(0, _chrs.Length)];
            }

            Debug.Log($"RANDOMLY CREATED CRYPTING KEYS (set them manually) (check the full log, to see iv aswell)\nKEY: {_key}\nIV: {_iv}");
        }
        if (ClearData)
        {
            ClearData = false;
            data = new();
            Save();
            UpdateDebug();
        }
        if (OpenDataFolder)
        {
            OpenDataFolder = false;
            EditorUtility.RevealInFinder(path);
        }
    }
#endif

    #endregion

    #region Static Funcs

    // SETS AND GETS

    public static void SetValue(string _key, object _o)
    {
        data[_key] = _o;
        Instance.UpdateDebug();
    }

    public static object GetValue(string _key)
    {
        return data.ContainsKey(_key) ? data[_key] : null;
    }



    // SAVE AND LOAD

    public static void Save()
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        if (!Instance.DisableCrypting) json = Encrypt(json);
        File.WriteAllText($"{path}{sep}data.json", json);
        #if UNITY_WEBGL && !UNITY_EDITOR 
        UpdateFileSystem();
        #endif
    }

    public static void Load()
    {
        if (File.Exists($"{path}{sep}data.json"))
        {
            try
            {
                string fileContents = File.ReadAllText($"{path}{sep}data.json");
                if (!Instance.DisableCrypting) fileContents = Decrypt(fileContents);
                data = JsonConvert.DeserializeObject<Dictionary<string, object>>(fileContents);
            }
            catch
            {
                Debug.LogWarning("Data Load Error: couldn't deserialize contents. was file crypted, but now crypting is disabled? or the other way around?");
            }
        }
        else
        {
            Save();
        }
    }



    // AES CRYPTING (keys are in private static vars) (create random keys with the help of the inspector)

    private static string Encrypt(string _input)
    {
        AesCryptoServiceProvider provider = GetAESProvider();

        byte[] txtByteData = ASCIIEncoding.ASCII.GetBytes(_input);
        ICryptoTransform trnsfrm = provider.CreateEncryptor(provider.Key, provider.IV);

        byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
        return Convert.ToBase64String(result);
    }

    private static string Decrypt(string _input)
    {
        AesCryptoServiceProvider provider = GetAESProvider();

        byte[] txtByteData = Convert.FromBase64String(_input);
        ICryptoTransform trnsfrm = provider.CreateDecryptor();

        byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
        return ASCIIEncoding.ASCII.GetString(result);
    }

    private static AesCryptoServiceProvider GetAESProvider()
    {
        AesCryptoServiceProvider AEScryptoProvider = new AesCryptoServiceProvider();
        AEScryptoProvider.BlockSize = 128;
        AEScryptoProvider.KeySize = 256;
        AEScryptoProvider.Key = ASCIIEncoding.ASCII.GetBytes(key);
        AEScryptoProvider.IV = ASCIIEncoding.ASCII.GetBytes(iv);
        AEScryptoProvider.Mode = CipherMode.CBC;
        AEScryptoProvider.Padding = PaddingMode.PKCS7;
        return AEScryptoProvider;
    }
#endregion
}
