using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candles : MonoBehaviour
{
    public List<SpriteRenderer> glows;
    public Timer timer;

    Animator[] m_Candles;
    
    int burningCount;
    float m_StartTime;

    // Start is called before the first frame update
    void Start()
    {
        m_StartTime = timer.timeRemaining;

        m_Candles = GetComponentsInChildren<Animator>();
        burningCount = m_Candles.Length;

        for (var i = 0; i < m_Candles.Length; i++)
        {
            var candle = m_Candles[i];
            candle.SetFloat("StartAt", Random.value);
            candle.SetTrigger("Burn");
            glows[i].gameObject.SetActive(false);
        }

        glows[m_Candles.Length - 1].gameObject.SetActive(true);
    }

    void Update()
    {
        if (burningCount > 0) {
            var timePercent = timer.timeRemaining / m_StartTime;
            var nextBurningPercentMilestone = ((burningCount - 1f) / m_Candles.Length);

            if (timePercent <= nextBurningPercentMilestone)
            {
                DecrementCount();
            }
        }
    }

    public void DecrementCount()
    {
        glows[burningCount - 1].gameObject.SetActive(false);
        m_Candles[burningCount - 1].SetTrigger("GoOut");
        SfxManager.Instance.PlayCandleOut();
        burningCount--;
        if(burningCount > 0)
        {
            glows[burningCount - 1].gameObject.SetActive(true);
        }
        MusicManager.Instance.PlayByCandleCount(burningCount);
    }
}
