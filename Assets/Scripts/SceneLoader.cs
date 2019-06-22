using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Start()
    {
        FadeManager.Instance.FadeIn(1);
    }

    public void FadeScene(string sceneName)
    {
        FadeManager.Instance.FadeOut(1, Color.black, () =>
        {
            StartCoroutine(LoadAsyncScene(sceneName));
        });
    }

    IEnumerator LoadAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}