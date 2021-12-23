using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class starts the game
/// </summary>
public class MineStartGame : MonoBehaviour
{
    public Text highScore;
    public GameLoader management;
    public GameObject background;
    public GameObject inGame;
    public GameObject game;
    public AudioSource buttonClip;

    private void Start()
    {
        Element.isLost = false;
        highScore.text = management.GetComponent<GameListDatabaseLoader>().gameList[GameClick.currentGameId].highestScore.ToString();
        Element.score = 0;
    }

    /// <summary>
    /// Play Button Function
    /// </summary>
    public void OnPlayButtonClick()
    {
        buttonClip.Play();
        gameObject.SetActive(false);
        background.SetActive(false);
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
