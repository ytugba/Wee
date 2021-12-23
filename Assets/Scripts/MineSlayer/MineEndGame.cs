using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Game over screen display
/// </summary>
public class MineEndGame : MonoBehaviour
{
    public AudioSource buttonClip;
    public Text score;
    public GameLoader management;
    public Text gameResultText;
	public Text mineInfo;

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
        if (Playfield.isFinished())
        {
            gameResultText.text = "YOU WIN";
            score.text = Element.score.ToString();
			mineInfo.text = "Wow! You survived in " + Element.mineCount + " mines. What a hero you are!";
        }
        if(Element.isLost)
        {
            gameResultText.text = "YOU LOSE";
            score.text = Element.score.ToString();
			mineInfo.text = "Poor you. There were only " + Element.mineCount + " mines...";
        }
    }
}
