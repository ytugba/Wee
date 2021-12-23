using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class is will be attached to the management objects on scenes
/// It creates loading screen on scene passing
/// </summary>
public class GameLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    /// <summary>
    /// Call this function via play button from inspector
    /// </summary>
    /// <param name="sceneIndex"> is loading the next scene</param>
    public void LoadGame(int sceneIndex)
    {
        StartCoroutine(LoadAsynch(sceneIndex));
    }

    public void PreviousPage(int sceneIndex)
    {
        StartCoroutine(LoadAsynch(sceneIndex - 1));
    }

    System.Collections.IEnumerator LoadAsynch(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;

            yield return null;
        }
    }
}
