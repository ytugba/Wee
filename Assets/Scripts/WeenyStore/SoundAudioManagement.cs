using UnityEngine;

/// <summary>
/// Mute/Unmute voices
/// </summary>
public class SoundAudioManagement : MonoBehaviour
{
    private void Start()
    {
        UpdateSoundAndAudioSettings();
    }

    public static void UpdateSoundAndAudioSettings()
    {
        if (SettingsManagement.isAudioMuted)
        {
            foreach (var source in FindObjectsOfType<GameObject>())
            {
                if (source.GetComponent<AudioSource>() != null)
                {
                    source.GetComponent<AudioSource>().mute = true;
                }
            }
        }
        else
        {
            foreach (var source in FindObjectsOfType<GameObject>())
            {
                if (source.GetComponent<AudioSource>() != null)
                {
                    source.GetComponent<AudioSource>().mute = false;
                }
            }
        }
        if(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>() != null)
        {
            if (SettingsManagement.isSoundMuted)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().mute = true;
            }
            else
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().mute = false;
            }
        }

    }
}

