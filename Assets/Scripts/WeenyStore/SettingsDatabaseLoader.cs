using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// This class saves/loads settings of the game
/// </summary>
[Serializable]
public class SettingsDatabaseLoader : MonoBehaviour
{
    [HideInInspector]
    public List<Settings> settings = new List<Settings>();

    private void Awake()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Settings.json";
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            string json = formatter.Deserialize(stream) as string;
            Settings[] list = JsonHelperSettings.FromJson<Settings>(json);
            settings = new List<Settings>(list);
            stream.Close();
        }
        else
        {
            TextAsset file = Resources.Load<TextAsset>("Databases/SettingsJSON");
            string jsonString = file.text;
            Settings[] list = JsonHelperSettings.FromJson<Settings>(jsonString);
            settings = new List<Settings>(list);
            FileStream stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, jsonString);
            stream.Close();
        }
    }

    public void SaveSettings(List<Settings> settings)
    {
        Settings[] settingsArray = settings.ToArray();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Settings.json";
        if (!File.Exists(path))
        {
            TextAsset file = Resources.Load<TextAsset>("Databases/SettingsJSON");
            string jsonString = file.text;
            FileStream stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, jsonString);
            stream.Close();
        }
        else
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            string json = JsonHelperSettings.ToJson(settingsArray);
            formatter.Serialize(stream, json);
            stream.Close();
        }
    }
}

/// <summary>
/// This class allows multiple data types in SettingsJSON file
/// </summary>
[Serializable]
public static class JsonHelperSettings
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Settings;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Settings = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Settings = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Settings;
    }
}
