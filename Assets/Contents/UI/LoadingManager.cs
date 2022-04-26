using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public enum SceneName
    {
        Null,
        MainMenu,
        TrainingRoom
    }

    private SceneName loadingSceneName;
    private AsyncOperation operation;

    public void ChangeScene(SceneName sceneName)
    {
        if(this.loadingSceneName == SceneName.Null)
        {
            UIUtility.ShowObject(gameObject);
            loadingSceneName = sceneName;
            StartCoroutine(LoadScene(sceneName));
        }
    }

    void Awake()
    {
        loadingSceneName = SceneName.Null;
    }

    private void Start()
    {
        UIUtility.HideObject(gameObject);
    }

    IEnumerator LoadScene(SceneName sceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        operation = SceneManager.LoadSceneAsync(sceneName.ToString());
        operation.completed += (AsyncOperation obj) =>
        {
            obj.allowSceneActivation = true;
        };

        // Wait until the asynchronous scene fully loads
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
