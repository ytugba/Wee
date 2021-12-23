using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Logic application on UI
/// </summary>
public class RPSLSGameUI : MonoBehaviour
{
    private int earnedLilliputian;

    public PlayerDatabaseLoader management;
    public Button pause;

    public Text earnedLilliputianText;
    public Text statusText;
    public Text notification;

    public GameObject weapons;
    public GameObject gameOverScreen;
    public GameObject inGame;

    [HideInInspector]
    public int playerScore = 0;
    [HideInInspector]
    public int opponentScore = 0;
    [HideInInspector]
    public bool isReady;

    private void Start()
    {
        notification.gameObject.SetActive(true);
        pause.gameObject.SetActive(false);
        weapons.SetActive(false);
        isReady = true;
    }

    private void Update()
    {
        if(isReady)
        {
            StartCoroutine(BeginGame());
        }

        if(opponentScore == 5 || playerScore == 5)
        {
            if (playerScore > opponentScore)
            {
				int fee;
				fee = Random.Range(1, 10);
                earnedLilliputian = 30 * (playerScore - opponentScore);
                earnedLilliputian += ((fee * earnedLilliputian) / 100);
            }
            else
            {
                earnedLilliputian = 0;
            }

            earnedLilliputianText.text = earnedLilliputian.ToString();
            inGame.SetActive(false);
            gameOverScreen.SetActive(true);
            management.player[0].lilliputian += earnedLilliputian;
            management.SavePlayerData(management.player);
        }
    }

    IEnumerator BeginGame()
    {
        notification.gameObject.SetActive(true);
        statusText.gameObject.SetActive(false);
        pause.gameObject.SetActive(false);
        isReady = false;

        yield return new WaitForSeconds(0.5f);

        notification.gameObject.SetActive(false);
        statusText.gameObject.SetActive(true);
        pause.gameObject.SetActive(true);
        weapons.SetActive(true);
    }
       
}
