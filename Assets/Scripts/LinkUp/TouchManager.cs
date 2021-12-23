using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 50.0f;
    private float maxSwipeTime = 1.5f;

    private GameManager gm;

    public AudioClip  musicClip;
    public AudioSource musicSource;
    void Awake()
    {
        gm = GameObject.FindObjectOfType<GameManager> ();
        musicSource.clip = musicClip;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            foreach(Touch touch in Input.touches)
            {
                switch(touch.phase)
                {
                    case TouchPhase.Began:
                        isSwipe = true;
                        fingerStartTime = Time.time;
                        fingerStartPos = touch.position;
                        break;
                    case TouchPhase.Canceled:
                        isSwipe = false;
                        break;
                    case TouchPhase.Ended:
                        float gestureTime = Time.time - fingerStartTime;
                        float gestureDist = (touch.position - fingerStartPos).magnitude;

                        if(isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist && !LinkUpPauseGame.isPaused)
                        {
                            Vector2 direction = touch.position - fingerStartPos;
                            Vector2 swipeType = Vector2.zero;

                            if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                            {
                                swipeType = Vector2.right * Mathf.Sign(direction.x);
                            }
                            else
                            {
                                swipeType = Vector2.up * Mathf.Sign(direction.y);
                            }

                            if(swipeType.x != 0.0f)
                            {
                                if(swipeType.x > 0.0f) //Move Right
                                {
                                    gm.Move(MoveDirection.Right);
                                }
                                else  //Move Left
                                {
                                    gm.Move(MoveDirection.Left);
                                }
                            }

                            if(swipeType.y != 0.0f)
                            {
                                if(swipeType.y > 0.0f)  //Move Up
                                {
                                    gm.Move(MoveDirection.Up);
                                }
                                else   //Move Down
                                {
                                    gm.Move(MoveDirection.Down);
                                }
                            }

                            musicSource.Play();

                        }
                        break;
                }
            }
        }
    }
}
