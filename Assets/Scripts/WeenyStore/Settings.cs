using Newtonsoft.Json;
using System;
[Serializable]
public class Settings
{
    public bool soundMute;
    public bool audioMute;
    //If needed other options can be added.

    #region Constructors

    private Settings()
    {
    }
    [JsonConstructor]
    public Settings(bool SoundMute, bool AudioMute)
    {
        audioMute = AudioMute;
        soundMute = SoundMute;

    }
    [JsonConstructor]
    public Settings(Settings settings)
    {
        this.soundMute = settings.soundMute;
        this.audioMute = settings.audioMute;
    }
    #endregion
}
