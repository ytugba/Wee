using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Game panel display
/// </summary>
public class SmashInGame : MonoBehaviour
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

    int earned = 0;

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
        scoreText.text = SmashPlayfield.score.ToString();
        if (Groups.isOver)
        {
			int highScore;
			highScore = management.GetComponent<GameListDatabaseLoader>().gameList[GameClick.currentGameId].highestScore;

            if(SmashPlayfield.score > highScore)
            {
				earned = (SmashPlayfield.score) + ((((highScore / 100) * 10) + 1));
                highScoreGameOver.text = SmashPlayfield.score.ToString();
                management.GetComponent<GameListDatabaseLoader>().gameList[GameClick.currentGameId].highestScore = SmashPlayfield.score;
                highScoreNotification.gameObject.SetActive(true);
                gameListDatabase.SaveGameListData(management.GetComponent<GameListDatabaseLoader>().gameList);
}
            else
            {
				earned = SmashPlayfield.score;
                highScoreNotification.gameObject.SetActive(false);
                highScoreGameOver.text = highScore.ToString();
            }
			
				playerDatabase.player[0].lilliputian += earned;
				earnedLilliputianText.text = earned.ToString();
				playerDatabase.SavePlayerData(playerDatabase.player);			
			
            foreach (var block in GameObject.FindGameObjectsWithTag("SmashBlock"))
            {
                Destroy(block);
            }
            game.SetActive(false);
            backgroundMenu.SetActive(true);
            gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
        }
    }
}
