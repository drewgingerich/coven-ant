using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Start()
    {
        //FadeManager.Instance.FadeIn(1f);
    }

    public void FadeScene(string sceneName)
    {
        FadeManager.Instance.FadeOut(0.5f, Color.black, () =>
        {
            StartCoroutine(LoadAsyncScene(sceneName));
            MusicManager.Instance.PlaySceneMusic(sceneName);
        });
        MusicManager.Instance.FadeOut(0.5f);
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