using UnityEngine;

/// <summary>
/// This class freezes the game progress
/// </summary>
public class SmashPauseGame : MonoBehaviour
{
    public AudioSource buttonClip;
    public GameLoader management;
    public GameObject game;
    public GameObject background;
    public GameObject inGame;

    /// <summary>
    /// Unfreeze game progress
    /// </summary>
    public void OnPlayButtonClick()
    {
        buttonClip.Play();
        game.SetActive(true);
        background.SetActive(false);
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
