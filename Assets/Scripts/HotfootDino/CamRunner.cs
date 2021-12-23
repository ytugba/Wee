using UnityEngine;

public class CamRunner : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Over.isOver == false)
        {
            transform.Translate(Vector3.right * PlayerPrefs.GetInt("speed") * Time.deltaTime);
        }
    }
}
