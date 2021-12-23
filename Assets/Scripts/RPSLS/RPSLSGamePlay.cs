using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Logic behind the game 
/// Script is loaded on player buttons
/// </summary>
public class RPSLSGamePlay : MonoBehaviour
{
    private string[] options = { "Rock","Paper","Scissors","Lizard","Spock" };
    private string opponentsChoice;
    private string playerChoice;
    public Text notification;
    public Text statusText;
    public GameObject weapons;
    public RPSLSGameUI game;
    public Button pauseButton;
    public AudioSource tieClip;
    public AudioSource winClip;
    public AudioSource lostClip;

    private bool isClicked;

    public void OnChoice()
    {
        playerChoice = EventSystem.current.currentSelectedGameObject.name;
        CompareResultsOnClick(playerChoice);
    }

    private void CompareResultsOnClick( string playerChoice)
    {
        if(!isClicked)
        {
            opponentsChoice = options[Random.Range(0, options.Length)];

            GameObject.Find(opponentsChoice + "_Enemy").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/RPSLS/" + opponentsChoice);

            if (playerChoice == opponentsChoice)
            {
                statusText.text = "TIE!";
                isClicked = true;
                tieClip.Play();
            }
            else
            {
                #region cases
                if (playerChoice == "Rock" && (opponentsChoice == "Lizard" || opponentsChoice == "Scissors"))
                {
                    game.playerScore += 1;
                    statusText.text = "YOU WIN!";
                    isClicked = true;
                    winClip.Play();
                }
                if(playerChoice == "Paper" && (opponentsChoice == "Rock" || opponentsChoice == "Spock"))
                {
                    game.playerScore += 1;
                    statusText.text = "YOU WIN!";
                    isClicked = true;
                    winClip.Play();
                }
                    if(playerChoice == "Scissors" && (opponentsChoice == "Paper" || opponentsChoice == "Lizard"))
                {
                    game.playerScore += 1;
                    statusText.text = "YOU WIN!";
                    isClicked = true;
                    winClip.Play();
                }
                if(playerChoice == "Lizard" && (opponentsChoice == "Paper" || opponentsChoice == "Spock"))
                {
                    game.playerScore += 1;
                    statusText.text = "YOU WIN!";
                    isClicked = true;
                    winClip.Play();
                }
                if(playerChoice == "Spock" && (opponentsChoice == "Rock" || opponentsChoice == "Scissors"))
                {
                    game.playerScore += 1;
                    statusText.text = "YOU WIN!";
                    isClicked = true;
                    winClip.Play();
                }

                if (opponentsChoice == "Rock" && (playerChoice == "Scissors" || playerChoice == "Lizard"))
                {
                    game.opponentScore += 1;
                    statusText.text = "YOU LOST!";
                    isClicked = true;
                    lostClip.Play();
                }
                if(opponentsChoice == "Paper" && (playerChoice == "Rock" || playerChoice == "Spock"))
                {
                    game.opponentScore += 1;
                    statusText.text = "YOU LOST!";
                    isClicked = true;
                    lostClip.Play();
                }
                if(opponentsChoice == "Scissors" && (playerChoice == "Paper" || playerChoice == "Lizard"))
                {
                    game.opponentScore += 1;
                    statusText.text = "YOU LOST!";
                    isClicked = true;
                    lostClip.Play();
                }
                if(opponentsChoice == "Lizard" && (playerChoice == "Paper" || playerChoice == "Spock"))
                {
                    game.opponentScore += 1;
                    statusText.text = "YOU LOST!";
                    isClicked = true;
                    lostClip.Play();
                }
                if(opponentsChoice == "Spock" && (playerChoice == "Rock" || playerChoice == "Scissors"))
                {
                    game.opponentScore += 1;
                    statusText.text = "YOU LOST!";
                    isClicked = true;
                    lostClip.Play();
                }
                #endregion
            }
            StartCoroutine(CheckWinOrLose());
        }
    }

    IEnumerator CheckWinOrLose()
    {
        pauseButton.interactable = false;
        yield return new WaitForSeconds(1.5f);
        pauseButton.interactable = true;
        GameObject.Find(opponentsChoice + "_Enemy").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/RPSLS/" + opponentsChoice + "_enemy");
        GameObject.Find("Game").GetComponent<RPSLSGameUI>().isReady = true;
        lostClip.Stop();
        winClip.Stop();
        tieClip.Stop();
        weapons.SetActive(false);
        notification.gameObject.SetActive(true);
        statusText.gameObject.SetActive(false);
        statusText.text = "";
        isClicked = false;
    }
}
