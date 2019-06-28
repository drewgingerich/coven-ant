using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    [Header("Fade references")]
    public CanvasGroup m_FadeGroup;
    public Image m_FadeImage;

    public CanvasGroup creditsParent;
    public Image credits;

    [Header("Start Fade")]
    [SerializeField] bool m_StartWithFade = true;
    [SerializeField] float m_StartFadeTime = 1.5f;
    [SerializeField] Color m_StartFadeColor = Color.black;

	Text m_Text;
    Image m_Credits;

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

    void Start()
    {
        m_Text = GetComponentInChildren<Text>();

        if (m_Text)
        {
            m_Text.color = new Color(m_Text.color.r, m_Text.color.g, m_Text.color.b, 0f);
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

    public void FadeOutText(float transitionTime, Action func)
    {
        StartCoroutine(UpdateFadeOutText(transitionTime, func));
    }

    public void ShowCredits(float transitionTime, float showCreditsTime, Action func)
    {
        StartCoroutine(UpdateShowCredits(transitionTime, showCreditsTime, func));
    }

    IEnumerator UpdateFadeOut(float transitionTime, Action func)
    {
        var t = 0f;

        if (m_Text)
        {
            m_Text.color = new Color(m_Text.color.r, m_Text.color.g, m_Text.color.b, 1f);
        }

        m_FadeGroup.alpha = 0f;

        for (t = 0f; t <= 1; t += Time.deltaTime / transitionTime)
        {
            m_FadeGroup.alpha = t;
            yield return null;
        }

        m_FadeGroup.alpha = 1f;
        func?.Invoke();
    }

    IEnumerator UpdateFadeOutText(float transitionTime, Action func)
    {
        var t = 0f;

        if (m_Text)
        {
            m_Text.color = new Color(m_Text.color.r, m_Text.color.g, m_Text.color.b, 1f);

            for (t = 0f; t <= 1; t += Time.deltaTime / transitionTime)
            {
                m_Text.color = new Color(m_Text.color.r, m_Text.color.g, m_Text.color.b, 1f - t);
                yield return null;
            }

            m_Text.color = new Color(m_Text.color.r, m_Text.color.g, m_Text.color.b, 0f);
            func?.Invoke();
        }
    }

    IEnumerator UpdateShowCredits(float transitionTime, float showCreditsTime, Action func)
    {
        var t = 0f;

        creditsParent.alpha = 0f;
        credits.color = new Color(credits.color.r, credits.color.g, credits.color.b, 1f);

        // Fade in credits and background
        for (t = 0f; t <= 1; t += Time.deltaTime / transitionTime)
        {
            creditsParent.alpha = t;
            yield return null;
        }

        creditsParent.alpha = 1f;

        // Show credits for a while
        yield return new WaitForSeconds(showCreditsTime);

        credits.color = new Color(credits.color.r, credits.color.g, credits.color.b, 1f);

        // Fade out credits
        for (t = 0f; t <= 1; t += Time.deltaTime / transitionTime)
        {
            credits.color = new Color(credits.color.r, credits.color.g, credits.color.b, 1f - t);
            yield return null;
        }

        credits.color = new Color(credits.color.r, credits.color.g, credits.color.b, 0f);

        func?.Invoke();
    }
}
