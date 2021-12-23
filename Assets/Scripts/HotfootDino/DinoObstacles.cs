using System.Collections.Generic;
using UnityEngine;

public class DinoObstacles : MonoBehaviour
{
    public GameObject[] ob;
    public GameObject dino;
    public List<GameObject> obstaclesInScene;
    public static int score = 0;
    float xx;
    GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        obstaclesInScene = new List<GameObject>();
        game = GameObject.Find("Game");
        ObstacleMaker();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Over.isOver == false && DinoInGame.isPaused == false))
        {
            transform.Translate(Vector3.right * PlayerPrefs.GetInt("speed") * Time.deltaTime);
            for(var i = obstaclesInScene.Count - 1; i > -1; i--)
            {
                if (obstaclesInScene[i] == null)
                   obstaclesInScene.RemoveAt(i);
            }
        }
        if (Over.isOver)
        {
            CancelInvoke();
        }
    }

    void ObstacleMaker()
    {
        if(!Over.isOver)
        {

            GameObject clone = (GameObject)Instantiate(ob[Random.Range(0, ob.Length)], transform.position, Quaternion.identity, game.transform);
            clone.name = "Quad";
            clone.tag = "Quad";
            clone.AddComponent<BoxCollider2D>();
            clone.GetComponent<BoxCollider2D>().isTrigger = true;
            obstaclesInScene.Add(clone);
            DestroyRecreateObstacles();
        }
    }

    void DestroyRecreateObstacles()
    {
        if (score > 30)
            xx = Random.Range(1, 3);
        else
            xx = Random.Range(2, 6);

        foreach (var obs in obstaclesInScene)
        {
            if (obs.transform.localPosition.x < dino.transform.localPosition.x)
            {
                if (obstaclesInScene.Contains(obs))
                {
                    Destroy(obs);
                }
            }
        }
        Invoke("ObstacleMaker", xx);
    }
}
