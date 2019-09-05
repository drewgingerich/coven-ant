using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IntroHandler : MonoBehaviour
{
    public List<GameObject> slides;
    public string nextSceneName;
    public SceneLoader sceneLoader;
    public Button backButton;
    public Button nextButton;

    private GameObject activeSlide;
    private int slideIndex;

    void Start()
    {
        activeSlide = slides[0];
        activeSlide.SetActive(true);

        for (int i = 1; i < slides.Count; i++)
        {
            slides[i].SetActive(false);
        }

        backButton.gameObject.SetActive(false);

        backButton.onClick.AddListener(Back);
        nextButton.onClick.AddListener(Next);
    }

    public void Next()
    {
        slideIndex++;
        if (slideIndex == slides.Count)
        {
            sceneLoader.FadeScene(nextSceneName);
        }
        else
        {
            activeSlide.SetActive(false);
            activeSlide = slides[slideIndex];
            activeSlide.SetActive(true);
        }
        backButton.gameObject.SetActive(true);
    }

    public void Back()
    {
        slideIndex--;

        Debug.Assert(slideIndex >= 0);

        activeSlide.SetActive(false);
        activeSlide = slides[slideIndex];
        activeSlide.SetActive(true);

        if (slideIndex == 0)
        {
            backButton.gameObject.SetActive(false);
            nextButton.Select();
            nextButton.OnSelect(null);
        }
    }
}
