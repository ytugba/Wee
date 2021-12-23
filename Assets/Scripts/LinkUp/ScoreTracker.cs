using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{

    public static int score;
    public static ScoreTracker instance;

    public int Score{

        get{
            return score;
        }

        set{
            score = value;
        }
    }

    void Awake(){

        instance = this;
    }
    
}
