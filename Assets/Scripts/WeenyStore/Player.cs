using Newtonsoft.Json;
using System;

/// <summary>
/// This class collects the data from the related json file and lets
/// information to be used in the scripts
/// </summary>
[Serializable]
public class Player
{
    public string userName;
    public int lilliputian;

    #region Constructors

    private Player()
    {
    }

    [JsonConstructor]
    public Player(string UserName, int Lilliputian)
    {
        userName = UserName;
        lilliputian = Lilliputian;
    }

    [JsonConstructor]
    public Player(Player player)
    {
        this.userName = player.userName;
        this.lilliputian = player.lilliputian;
    }
    #endregion
}