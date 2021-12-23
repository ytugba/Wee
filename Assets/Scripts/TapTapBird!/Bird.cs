using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float upForce = 200f;

    private bool isDead = false;
    private Rigidbody2D rb2d;
    public AudioSource wingSound;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
    }

    void Update()
    {
        if(isDead == false)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(rb2d.position.y < 4.2){
                    rb2d.velocity = Vector2.zero ;
                    rb2d.AddForce(new Vector2(0,upForce));  
                    wingSound.Play();  
                }
            }
        }
    }

    void OnCollisionEnter2D()
    {
        rb2d.velocity = Vector2.zero;
        isDead = true;
        GameControl.instance.BirdDied();
    }
}
