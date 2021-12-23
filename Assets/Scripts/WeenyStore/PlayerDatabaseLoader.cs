using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// This class deserializes the PlayerDataJSON file
/// </summary>
[Serializable]
public class PlayerDatabaseLoader : MonoBehaviour
{
    [HideInInspector]
    public Player[] player;

    private void Awake()
    {
        LoadPlayer();
    }

    public void LoadPlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/PlayerData.json";
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            string json = formatter.Deserialize(stream) as string;
            player = JsonHelperPlayer.FromJson<Player>(json);
            stream.Close();
        }
        else
        {
            TextAsset file = Resources.Load<TextAsset>("Databases/PlayerDataJSON");
            string jsonString = file.text;
            player = JsonHelperPlayer.FromJson<Player>(jsonString);
            FileStream stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, jsonString);
            stream.Close();
        }
    }

    public void SavePlayerData(Player[] player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/PlayerData.json";
        if(!File.Exists(path))
        {
            TextAsset file = Resources.Load<TextAsset>("Databases/PlayerDataJSON");
            string jsonString = file.text;
            FileStream stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, jsonString);
            stream.Close();
        }
        else
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            string json = JsonHelperPlayer.ToJson(player, true);
            formatter.Serialize(stream, json);
            stream.Close();
        }
    }
}

/// <summary>
/// This class allows multiple data types in GameListJSON file
/// </summary>
[Serializable]
public static class JsonHelperPlayer
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Player;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Player = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Player = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Player;
    }
}