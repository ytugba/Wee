using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Game over screen display
/// </summary>
public class TapTapBirdEndGame : MonoBehaviour
{
    public AudioSource buttonClip;
    public Text score;
    public GameLoader management;

    /// <summary>
    /// Resets the scene
    /// </summary>
    public void OnReplayButtonClick()
    {
        buttonClip.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Loads Weeny Store
    /// </summary>
    public void OnHomeButtonClick()
    {
        buttonClip.Play();
        management.LoadGame(1);
    }

    /// <summary>
    /// Displays last score on the game
    /// </summary>
    private void FixedUpdate()
    {
       score.text = GameControl.score.ToString();
    }
}
