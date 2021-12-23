using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Over : MonoBehaviour
{
    public static bool isOver = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Quad")
        {
            isOver = true;
            DinoInGame.isPaused = false;
            //print("game over");
        }
    }

}

