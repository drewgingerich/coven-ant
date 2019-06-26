using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    [Header("Fade references")]
    [SerializeField] CanvasGroup m_FadeGroup;
    [SerializeField] Image m_FadeImage;

    [Header("Start Fade")]
    [SerializeField] bool m_StartWithFade = true;
    [SerializeField] float m_StartFadeTime = 1.5f;
    [SerializeField] Color m_StartFadeColor = Color.black;

    void Awake()
    {
        Instance = this;

        //DontDestroyOnLoad(this.gameObject);

        if (m_StartWithFade)
        {
            FadeIn(m_StartFadeTime, m_StartFadeColor);
        } else
        {
            m_FadeGroup.alpha = 0f;
        }
    }

    public void FadeIn(float transitionTime)
    {
        FadeIn(transitionTime, m_FadeImage.color, null);
    }

    public void FadeIn(float transitionTime, Color fadeColor)
    {
        FadeIn(transitionTime, fadeColor, null);
    }

    public void FadeIn(float transitionTime, Color fadeColor, Action func)
    {
        m_FadeImage.color = fadeColor;
        StartCoroutine(UpdateFadeIn(transitionTime, func));
    }

    IEnumerator UpdateFadeIn(float transitionTime, Action func)
    {
        var t = 0f;

        m_FadeGroup.alpha = 1f;

        for (t = 0f; t <= 1; t += Time.deltaTime / transitionTime)
        {
            m_FadeGroup.alpha = 1 - t;
            yield return null;
        }

        m_FadeGroup.alpha = 0f;
        func?.Invoke();
    }


    public void FadeOut(float transitionTime)
    {
        FadeOut(transitionTime, m_FadeImage.color, null);
    }

    public void FadeOut(float transitionTime, Color fadeColor)
    {
        FadeOut(transitionTime, fadeColor, null);
    }

    public void FadeOut(float transitionTime, Color fadeColor, Action func)
    {
        m_FadeImage.color = fadeColor;
        StartCoroutine(UpdateFadeOut(transitionTime, func));
    }

    IEnumerator UpdateFadeOut(float transitionTime, Action func)
    {
        var t = 0f;

        m_FadeGroup.alpha = 0f;

        for (t = 0f; t <= 1; t += Time.deltaTime / transitionTime)
        {
            m_FadeGroup.alpha = t;
            yield return null;
        }

        m_FadeGroup.alpha = 1f;
        func?.Invoke();
    }
}
