using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public float fadeTime = 0.5f;
    public float waitAfterFade = 0f;

    private void Start()
    {
        //FadeManager.Instance.FadeIn(1f);
    }

    public void FadeScene(string sceneName)
    {
        FadeManager.Instance.FadeOut(0.5f, Color.black, () =>
        {
            StartCoroutine(LoadAsyncScene(sceneName));
            //MusicManager.Instance.PlaySceneMusic(sceneName);
        });
        MusicManager.Instance.FadeOut(0.5f);
    }

    public void FadeSceneWithCredits(string sceneName)
    {
        FadeManager.Instance.FadeOut(0.5f, Color.black, () =>
        {
            StartCoroutine(LoadAsyncSceneWithCredits(sceneName));
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

    IEnumerator LoadAsyncSceneWithCredits(string sceneName)
    {
        yield return new WaitForSeconds(waitAfterFade);

        FadeManager.Instance.FadeOutText(fadeTime, () =>
        {
            StartCoroutine(LoadAsyncScene(sceneName));
        });
    }
}