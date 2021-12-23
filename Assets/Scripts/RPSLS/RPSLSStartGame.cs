using UnityEngine;

/// <summary>
/// This class starts the game
/// </summary>
public class RPSLSStartGame : MonoBehaviour
{
    public GameLoader management;
    public GameObject inGame;
    public GameObject game;
    public AudioSource buttonClip;

    /// <summary>
    /// Play Button Function
    /// </summary>
    public void OnPlayButtonClick()
    {
        buttonClip.Play();
        gameObject.SetActive(false);
        game.SetActive(true);
        inGame.SetActive(true);
    }

    /// <summary>
    /// Goes back to the Weeny Store
    /// </summary>
    public void OnHomeButtonClick()
    {
        buttonClip.Play();
        management.LoadGame(1);
    }
}
