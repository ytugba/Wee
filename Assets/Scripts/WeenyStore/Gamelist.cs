using Newtonsoft.Json;
using System;

/// <summary>
/// This class collects the data from the related json file and lets
/// information to be used in the scripts
/// </summary>
[Serializable]
public class Gamelist
{
    public int id;
    public string title;
    public bool isLocked;
    public int highestScore;
    public int price;
    public string thumbnailPath;

    #region Constructors

    private Gamelist()
    {
    }
    [JsonConstructor]
    public Gamelist(int Id, string Title, bool IsLocked, int HighestScore, int Price , string ThumbnailPath)
    {
        id = Id;
        title = Title;
        isLocked = IsLocked;
        highestScore = HighestScore;
        price = Price;
        thumbnailPath = ThumbnailPath;
    }
    [JsonConstructor]
    public Gamelist(Gamelist gameList)
    {
        this.id = gameList.id;
        this.title = gameList.title;
        this.isLocked = gameList.isLocked;
        this.highestScore = gameList.highestScore;
        this.price = gameList.price;
        this.thumbnailPath = gameList.thumbnailPath;
    }
    #endregion
}

