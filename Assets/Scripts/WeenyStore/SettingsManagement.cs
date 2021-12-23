using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class adjusts the settings of the game
/// </summary>
public class SettingsManagement : MonoBehaviour
{
    public GameObject notification;
    public Button audioButton;
    public Button soundButton;
    public Sprite volumeDown;
    public Sprite volumeUp;
    public static bool isAudioMuted;
    public static bool isSoundMuted;

    private void Start()
    {
        notification.SetActive(false);

        if (!GetComponent<SettingsDatabaseLoader>().settings[0].audioMute)
        {
            audioButton.GetComponent<Image>().sprite = volumeUp;
            isAudioMuted = false;
        }
        else
        {
            audioButton.GetComponent<Image>().sprite = volumeDown;
            isAudioMuted = true;
        }

        if (!GetComponent<SettingsDatabaseLoader>().settings[0].soundMute)
        {
            soundButton.GetComponent<Image>().sprite = volumeUp;
            isSoundMuted = false;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = volumeDown;
            isSoundMuted = true;
        }
    }

    public void OnAudioClick()
    {
        if(!GetComponent<SettingsDatabaseLoader>().settings[0].audioMute)
        {
            GetComponent<SettingsDatabaseLoader>().settings[0].audioMute = true;
            isAudioMuted = true;
            audioButton.GetComponent<Image>().sprite = volumeDown;
        }
        else
        {
            GetComponent<SettingsDatabaseLoader>().settings[0].audioMute = false;
            isAudioMuted = false;
            audioButton.GetComponent<Image>().sprite = volumeUp;
        }
        GetComponent<SettingsDatabaseLoader>().SaveSettings(GetComponent<SettingsDatabaseLoader>().settings);
        SoundAudioManagement.UpdateSoundAndAudioSettings();
    }
    public void OnSoundClick()
    {
        if (!GetComponent<SettingsDatabaseLoader>().settings[0].soundMute)
        {
            GetComponent<SettingsDatabaseLoader>().settings[0].soundMute = true;
            isSoundMuted = true;
            soundButton.GetComponent<Image>().sprite = volumeDown;
        }
        else
        {
            GetComponent<SettingsDatabaseLoader>().settings[0].soundMute = false;
            isSoundMuted = false;
            soundButton.GetComponent<Image>().sprite = volumeUp;
        }
        GetComponent<SettingsDatabaseLoader>().SaveSettings(GetComponent<SettingsDatabaseLoader>().settings);
        SoundAudioManagement.UpdateSoundAndAudioSettings();
    }
    public void OnResetClick()
    {
        string settingsPath = Application.persistentDataPath + "/Settings.json";
        string playerPath = Application.persistentDataPath + "/PlayerData.json";
        string gameListPath = Application.persistentDataPath + "/GameList.json";

        string[] pathArray = {settingsPath, playerPath, gameListPath};

        foreach(var path in pathArray)
        {
            if(File.Exists(path))
            {
                StartCoroutine(ResetProgress(path));
            }
        }
    }

    IEnumerator ResetProgress(string path)
    {
        File.Delete(path);
        notification.SetActive(true);
        yield return new WaitForSeconds(3f);
        notification.SetActive(false);
    }
}
