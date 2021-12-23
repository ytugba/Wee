using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class starts the game
/// </summary>
public class DinoStartGame : MonoBehaviour
{
    public Text highScore;
    public GameLoader management;
    public GameObject background;
    public GameObject inGame;
    public GameObject game;
    public AudioSource buttonClip;
    public static int initialWaitTime;
    bool startMenu;

    private void Awake()
    {
        Over.isOver = true;
        DinoObstacles.score = 0;
        inGame.SetActive(false);
    }
    private void Start()
    {
        startMenu = true;
        highScore.text = management.GetComponent<GameListDatabaseLoader>().gameList[GameClick.currentGameId].highestScore.ToString();
    }

    private void Update()
    {
        if(startMenu)
        {
            initialWaitTime = (int)Time.timeSinceLevelLoad;
        }
    }

    /// <summary>
    /// Play Button Function
    /// </summary>
    public void OnPlayButtonClick()
    {
        startMenu = false;
        Over.isOver = false;
        DinoObstacles.score = (int)Time.timeSinceLevelLoad - initialWaitTime;
        buttonClip.Play();
        gameObject.SetActive(false);
        background.SetActive(false);
        inGame.SetActive(true);
        game.SetActive(true);
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
