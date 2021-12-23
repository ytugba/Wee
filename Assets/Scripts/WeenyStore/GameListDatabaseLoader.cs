using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// This class deserializes the GameListJSON file
/// </summary>
[Serializable]
public class GameListDatabaseLoader : MonoBehaviour
{
    [HideInInspector]
    public List<Gamelist> gameList = new List<Gamelist>();

    private void Awake()
    {
        LoadDatabase();
        Application.targetFrameRate = 60;
        
    }

    public void LoadDatabase()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/GameList.json";
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            string json = formatter.Deserialize(stream) as string;
            Gamelist[] list = JsonHelper.FromJson<Gamelist>(json);
            gameList = new List<Gamelist>(list);
            stream.Close();
        }
        else
        {
            TextAsset file = Resources.Load<TextAsset>("Databases/GameListJSON");
            string jsonString = file.text;
            Gamelist[] list = JsonHelper.FromJson<Gamelist>(jsonString);
            gameList = new List<Gamelist>((Gamelist[])list);
            FileStream stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, jsonString);
            stream.Close();
        }
    }

    public void SaveGameListData(List<Gamelist> gamelist)
    {
        Gamelist[] gameArray = gamelist.ToArray();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/GameList.json";

        if (!File.Exists(path))
        {
            TextAsset file = Resources.Load<TextAsset>("Databases/GameListJSON");
            string jsonString = file.text;
            FileStream stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, jsonString);
            stream.Close();
        }
        else
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            string json = JsonHelper.ToJson<Gamelist>(gameArray);
            formatter.Serialize(stream, json);
            stream.Close();
        }
    }
}

/// <summary>
/// This class allows multiple data types in GameListJSON file
/// </summary>
[Serializable]
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Gamelist;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Gamelist = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Gamelist = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Gamelist;
    }
}