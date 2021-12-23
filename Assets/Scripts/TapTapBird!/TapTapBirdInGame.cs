using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Game panel display
/// </summary>
public class TapTapBirdInGame : MonoBehaviour
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
   
    int earned = 0;

    /// <summary>
    /// Update scoreboard in game
    /// </summary>
    private void FixedUpdate()
    {
        scoreText.text = GameControl.score.ToString();
        if (GameControl.gameOver == true)
        {
            if(GameControl.score > management.GetComponent<GameListDatabaseLoader>().gameList[GameClick.currentGameId].highestScore)
            {
				earned = ((GameControl.score) + ((((management.GetComponent<GameListDatabaseLoader>().gameList[GameClick.currentGameId].highestScore / 100) * 10) + 1)) * 10);
                highScoreGameOver.text = GameControl.score.ToString();
                management.GetComponent<GameListDatabaseLoader>().gameList[GameClick.currentGameId].highestScore = GameControl.score;
                highScoreNotification.gameObject.SetActive(true);
                gameListDatabase.SaveGameListData(management.GetComponent<GameListDatabaseLoader>().gameList);
}
            else
            {
				earned = GameControl.score * 10;
                highScoreNotification.gameObject.SetActive(false);
                highScoreGameOver.text = management.GetComponent<GameListDatabaseLoader>().gameList[GameClick.currentGameId].highestScore.ToString();
            }
            playerDatabase.player[0].lilliputian += earned;
            earnedLilliputianText.text = earned.ToString();
            playerDatabase.SavePlayerData(playerDatabase.player);            
            game.SetActive(false);
            backgroundMenu.SetActive(true);
            gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
        }
    }
}
