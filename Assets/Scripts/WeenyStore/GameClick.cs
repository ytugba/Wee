using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClick : MonoBehaviour
{
    private GameObject management;
    private GameListDatabaseLoader gameListDatabase;
    private Text notification;
    private Sprite image;
    public static int gameId;
    public static int currentGameId;

    private void Awake()
    {
        notification = GameObject.Find("Notification").GetComponent<Text>();
        management = GameObject.Find("Management");
        gameListDatabase = management.GetComponent<GameListDatabaseLoader>();
    }

    private void Start()
    {
        notification.gameObject.SetActive(false);
    }

    /// <summary>
    /// Behaviour of the button click
    /// Scene index in build settings will be equal to the scene id
    /// </summary>
    public void OnClick()
    {
        foreach(var game in gameListDatabase.gameList)
        {
            if(game.id.ToString() == gameObject.name && !game.isLocked)
            {
                //START NEW GAME
                game.price = 0;
                gameId = game.id + 1;
                currentGameId = game.id - 1;
                management.GetComponent<GameLoader>().LoadGame(gameId);
            }

            //Buy new game
            if (game.id.ToString() == gameObject.name && game.isLocked && game.price <= management.GetComponent<PlayerDatabaseLoader>().player[0].lilliputian)
            {
                game.isLocked = false;
                management.GetComponent<PlayerDatabaseLoader>().player[0].lilliputian -= game.price;

                game.price = 0;
                image = Resources.Load<Sprite>("Images/Thumbnails/" + game.thumbnailPath);
                gameObject.GetComponent<Image>().sprite = image;
                gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = game.title;
                management.GetComponent<GameListDatabaseLoader>().SaveGameListData(gameListDatabase.gameList);
                management.GetComponent<PlayerDatabaseLoader>().SavePlayerData(management.GetComponent<PlayerDatabaseLoader>().player);;
            }

            else if (game.id.ToString() == gameObject.name && game.isLocked && game.price > management.GetComponent<PlayerDatabaseLoader>().player[0].lilliputian)
            {
                int need = game.price - management.GetComponent<PlayerDatabaseLoader>().player[0].lilliputian;
                StartCoroutine(HideMessage(need));
                //Debug.Log("Insufficient Lilliputian.");
            }
        }

    }

    IEnumerator HideMessage(int need)
    {
        notification.gameObject.SetActive(true);
        notification.text = $"Insufficient Lilliputian!\nYou need {need} more!";
        yield return new WaitForSeconds(2f);
        notification.gameObject.SetActive(false);
    }

    public void OnPreviousClick()
    {
        management.GetComponent<GameLoader>().PreviousPage(SceneManager.GetActiveScene().buildIndex);
    }
}
