using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    // Is this a mine?
    public bool mine;
    public static bool isLost;
    public GameObject game;
    public int click = 0;

    // Different textures
    public Sprite[] emptyTextures;
    public Sprite mineTexture;
    public Sprite flagTexture;

    public static int score = 0;
    public int scoreCount = 0;
	public static int mineCount;
    public int remainingMine;
    private int clickCount = 0;

    public AudioSource clickSound;
    public AudioSource boomSound;

    // Start is called before the first frame update
    void Start()
    {
        // Randomly decide if it's a mine or not
        mine = Random.value < 0.15; // 15% probability that an element is a mine

        // Register in Grid
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        Playfield.elements[x, y] = this;		
    }

    public void loadTexture(int adjacentCount)
    {
        if (mine)
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        else
        {
            scoreCount++;
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];

        }  		
    }
	
	public static int NumberOfMines()
	{
		int mineCount = 0;
		foreach (Element elem in Playfield.elements)
        {    if (elem.mine)
			{
				mineCount++;
			}
		}
		return mineCount;
	}
	
    // Is it still covered?
    public bool isCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == "pixel";
    }

	// Uncover all Mines
    IEnumerator UncoverMines()
    {
        foreach (Element elem in Playfield.elements)
        {
        	elem.gameObject.GetComponent<Collider2D>().enabled = false;
            if (elem.mine) 
            	elem.loadTexture(0);
        }
		yield return new WaitForSeconds(2f);
        if (score < 0)
            score = 0;
		game.SetActive(false);
		isLost = true;
	}
	
    void OnMouseUpAsButton()
    {
        clickCount++;
        mineCount = NumberOfMines();
        remainingMine = mineCount;
        click++;
        if (click == 1)
        {
            clickSound.Play();
            GetComponent<SpriteRenderer>().sprite = flagTexture;
            remainingMine--;
        }

        if (click > 1)
        {
            clickSound.Play();
            click = 0;
            // It's a mine
            if (mine)
            {
                if ((score - remainingMine) <= 0)
                { }
                else
                {
                    score -= remainingMine;
                } 
                boomSound.Play();
                // uncover all mines
                StartCoroutine(UncoverMines());
            }
            // It's not a mine
            else
            {
                remainingMine++;
                // show adjacent mine number
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;
                loadTexture(Playfield.adjacentMines(x, y));

                score += Playfield.adjacentMines(x, y) + scoreCount;

                scoreCount = 0;
                // uncover area without mines
                Playfield.FFuncover(x, y, new bool[Playfield.w, Playfield.h]);

                // find out if the game was won now
                if (Playfield.isFinished())
                {
                    score = score + (130-clickCount);
                    isLost = false;
                    game.SetActive(false);
                    //print("you win");
                }
            }
        }
    }
}
