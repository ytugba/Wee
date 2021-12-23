using UnityEngine;

/// <summary>
/// This class freezes the game progress
/// </summary>
public class RPSLSPauseGame : MonoBehaviour
{
    public AudioSource buttonClip;
    public GameLoader management;
    public GameObject inGame;

    /// <summary>
    /// Unfreeze game progress
    /// </summary>
    public void OnPlayButtonClick()
    {
        buttonClip.Play();
        gameObject.SetActive(false);
        inGame.SetActive(true);
    }

    /// <summary>
    /// Load Weeny Store
    /// </summary>
    public void OnHomeButtonClick()
    {
        buttonClip.Play();
        management.LoadGame(1);
    }
}
