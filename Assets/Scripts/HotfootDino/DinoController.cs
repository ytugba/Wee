using UnityEngine;

public class DinoController : MonoBehaviour
{
    public bool falling = false;
    public float yaxis;
	public AudioSource jumpSound;

    private void Start()
    {
        if(Over.isOver == false)
        {
            yaxis = GetComponent<Rigidbody2D>().velocity.y;
            falling = true;
            PlayerPrefs.SetInt("speed", 5);
        }
    }

    private void Update()
    {
        if(Over.isOver == false)
        {
            DinoObstacles.score = (int) Time.timeSinceLevelLoad - DinoStartGame.initialWaitTime;
            transform.Translate(Vector3.right * PlayerPrefs.GetInt("speed") * Time.deltaTime);
            if (Input.GetMouseButtonDown(0) && falling == false)
            {
                falling = true;
				jumpSound.Play();
                GetComponent<Rigidbody2D>().AddForce(new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 600.0f));
            }
        }
    }

    private void OnCollisionEnter2D()
    {
        falling = false;
    }
}
