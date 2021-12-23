using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Game panel display
/// </summary>
public class RPSLSInGame : MonoBehaviour
{
    public AudioSource buttonClip;
    public RPSLSGameUI game;
    public Text playerScoreText;
    public Text opponentScoreText;
    public GameObject pauseScreen;
    
    void Start()
    {
        pauseScreen.SetActive(false);
    }

    /// <summary>
    /// Freeze game progress
    /// </summary>
    public void OnPauseButtonClick()
    {
        buttonClip.Play();
        gameObject.SetActive(false);
        pauseScreen.SetActive(true);
    }

    /// <summary>
    /// Update scoreboard in game
    /// </summary>
    private void FixedUpdate()
    {
        playerScoreText.text = game.playerScore.ToString();
        opponentScoreText.text = game.opponentScore.ToString();
    }
}
