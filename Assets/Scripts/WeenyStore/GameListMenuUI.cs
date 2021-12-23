using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class adjusts the Menu UI according to the database values
/// </summary>
public class GameListMenuUI : MonoBehaviour
{
    private Sprite image;
    private string title;

    public Text lilliputianText;
    public Button[] gameListUI;
    public Sprite lockImage;

    private void Start()
    {
        for (int i = 0; i < gameListUI.Length; i++)
        {
            foreach(var game in gameObject.GetComponent<GameListDatabaseLoader>().gameList)
            {
                if(game.id.ToString() == gameListUI[i].gameObject.name)
                {
                    if(game.isLocked)
                    {
                        image = lockImage;
                        title = game.price.ToString();
                    }
                    else
                    {
                        image = Resources.Load<Sprite>("Images/Thumbnails/" + game.thumbnailPath);
                        title = game.title;
                    }

                    gameListUI[i].gameObject.GetComponent<Image>().sprite = image;
                    gameListUI[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = title;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        lilliputianText.text = gameObject.GetComponent<PlayerDatabaseLoader>().player[0].lilliputian.ToString();
    }
}
