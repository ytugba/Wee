using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Game panel display
/// </summary>
public class DinoInGame : MonoBehaviour
{
    public AudioSource buttonClip;
    public GameObject game;
    public Text scoreText;
    public GameObject background;
    public GameObject backgroundMenu;
    public GameObject gameOverScreen;
    public Text highScoreGameOver;
    public GameObject management;
    public Text highScoreNotification;
    public PlayerDatabaseLoader playerDatabase;
    public GameListDatabaseLoader gameListDatabase;
    public Text earnedLilliputianText;
    public static bool isPaused = false;


    void Start()
    {
        scoreText.text = DinoObstacles.score.ToString();
    }

    /// <summary>
    /// Freeze game progress
    /// </summary>
    public void OnPauseButtonClick()
    {
        isPaused = true;
        buttonClip.Play();
        game.SetActive(false);
        backgroundMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Update scoreboard in game
    /// </summary>
    private void FixedUpdate()
    {
        scoreText.text = DinoObstacles.score.ToString();
        if (Over.isOver)
        {
			int earned;
			earned = DinoObstacles.score * 2;
            playerDatabase.player[0].lilliputian += earned;
            earnedLilliputianText.text = earned.ToString();
            playerDatabase.SavePlayerData(playerDatabase.player);
            if (DinoObstacles.score > management.GetComponent<GameListDatabaseLoader>().gameList[GameClick.currentGameId].highestScore)
            {
                highScoreGameOver.text = DinoObstacles.score.ToString();
                management.GetComponent<GameListDatabaseLoader>().gameList[GameClick.currentGameId].highestScore = DinoObstacles.score;
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
