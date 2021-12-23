using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed,0);
    }

    
    void Update()
    {
        if(GameControl.gameOver == true )
        {
            rb2d.velocity = Vector2.zero;   // speed is 0 when game is over
        }
    }
}
