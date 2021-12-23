using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public static bool gameOver = false;
    public Text scoreText;
    public float scrollSpeed = -1.5f;

    public static int score = 0;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    
    void Update()
    {
        if(gameOver == true && Input.GetMouseButtonDown(0)) // restarting game
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void BirdScored()
    {
        if(gameOver)
        {
            return;
        }
        score = score + 1;
        scoreText.text = score.ToString();
    }

    public void BirdDied()      // Game over
    {
        gameOver = true;
    }
}
