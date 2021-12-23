using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Game panel display
/// </summary>
public class MineInGame : MonoBehaviour
{
    public AudioSource buttonClip;
    public GameObject game;
    public Text scoreText;
    public GameObject background;
    public GameObject backgroundMenu;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public Text highScoreGameOver;
    public GameObject management;
    public Text highScoreNotification;
    public PlayerDatabaseLoader playerDatabase;
    public GameListDatabaseLoader gameListDatabase;
    public Text earnedLilliputianText;


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
        game.SetActive(false);
        backgroundMenu.SetActive(true);
        gameObject.SetActive(false);
        pauseScreen.SetActive(true);
    }

    /// <summary>
    /// Update scoreboard in game
    /// </summary>
    private void FixedUpdate()
    {
        scoreText.text = Element.score.ToString();
        if (Element.isLost || Playfield.isFinished())
        {
            playerDatabase.player[0].lilliputian += Element.score;
            earnedLilliputianText.text = (Element.score).ToString(); 
            playerDatabase.SavePlayerData(playerDatabase.player);
            if (Element.score > management.GetComponent<GameListDatabaseLoader>().gameList[GameClick.currentGameId].highestScore)
            {
                highScoreGameOver.text = Element.score.ToString();
                management.GetComponent<GameListDatabaseLoader>().gameList[GameClick.currentGameId].highestScore = Element.score;
                highScoreNotification.gameObject.SetActive(true);
                gameListDatabase.SaveGameListData(management.GetComponent<GameListDatabaseLoader>().gameList);
            }
            else
            {
                highScoreNotification.gameObject.SetActive(false);
                highScoreGameOver.text = management.GetComponent<GameListDatabaseLoader>().gameList[GameClick.currentGameId].highestScore.ToString();
            }

            game.SetActive(false);
            backgroundMenu.SetActive(true);
            gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
        }
    }
}
