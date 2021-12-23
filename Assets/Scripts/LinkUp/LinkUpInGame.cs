using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Game panel display
/// </summary>
public class LinkUpInGame : MonoBehaviour
{
    public AudioSource buttonClip;
    public GameObject game;
    public Text scoreText;
    public GameObject background;
    public GameObject backgroundMenu;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public Text highScoreGameOver;
    public Text highScoreInGame;
    public GameObject management;
    public Text highScoreNotification;
    public PlayerDatabaseLoader playerDatabase;
    public GameListDatabaseLoader gameListDatabase;
    public Text earnedLilliputianText;
    private int currentHighScore;


    void Start()
    {
        pauseScreen.SetActive(false);
        highScoreInGame.text =  management.GetComponent<GameListDatabaseLoader>().gameList[GameClick.currentGameId].highestScore.ToString();
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
        LinkUpPauseGame.isPaused = true;
    }

    /// <summary>
    /// Update scoreboard in game
    /// </summary>
    private void FixedUpdate()
    {
        scoreText.text = ScoreTracker.instance.Score.ToString();
        currentHighScore = Int32.Parse(highScoreInGame.text);
    if(ScoreTracker.instance.Score > currentHighScore)
    {
        highScoreInGame.text = ScoreTracker.instance.Score.ToString();
    }

        if (GameManager.isOver)
        {
            playerDatabase.player[0].lilliputian +=Convert.ToInt32(ScoreTracker.instance.Score * 0.28 / 2);
            earnedLilliputianText.text =Convert.ToInt32(ScoreTracker.instance.Score * 0.28 / 2).ToString();
            playerDatabase.SavePlayerData(playerDatabase.player);
            if(ScoreTracker.instance.Score > management.GetComponent<GameListDatabaseLoader>().gameList[GameClick.currentGameId].highestScore)
            {
                highScoreGameOver.text = ScoreTracker.instance.Score.ToString();
                management.GetComponent<GameListDatabaseLoader>().gameList[GameClick.currentGameId].highestScore = ScoreTracker.instance.Score;
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
