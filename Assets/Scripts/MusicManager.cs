using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    public AudioClip titleMusic;
    public AudioClip introMusic;
    public AudioClip mainTheme50bpm;
    public AudioClip mainTheme70bpm;
    public AudioClip mainTheme90bpm;
    public AudioClip hallOfFame;

    AudioSource m_AudioSource;
    float m_TimeMultiplier70bpm = 7 / 5;
    float m_TimeMultiplier90bpm = 9 / 5;

    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
            return;
        } else
        {
            Instance = this;
        }
        //DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();

        PlaySceneMusic();
    }

    public void FadeOut(float transitionTime)
    {
        m_AudioSource.DOFade(0f, transitionTime);
    }

    void PlayTrack(AudioClip clip, float time = 0f)
    {
        m_AudioSource.volume = 1f;
        m_AudioSource.clip = clip;
        m_AudioSource.Play();
        m_AudioSource.time = time;
    }

    public void PlaySceneMusic(string sceneName = null)
    {
        sceneName = sceneName ?? SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "MainMenu":
                PlayTrack(titleMusic);
                break;
            case "Introduction":
                PlayTrack(introMusic);
                break;
            case "HallOfFame":
                PlayTrack(hallOfFame);
                break;
            default:
                PlayTrack(mainTheme50bpm);
                break;
        }
    }

    public void PlayByCandleCount(int candleCount)
    {
        switch (candleCount)
        {
            case 3:
                PlayTrack(mainTheme50bpm, m_AudioSource.time);
                break;
            case 2:
                PlayTrack(mainTheme70bpm, m_AudioSource.time * m_TimeMultiplier70bpm);
                break;
            case 1:
                PlayTrack(mainTheme90bpm, m_AudioSource.time * m_TimeMultiplier90bpm);
                break;
            default:
                PlayTrack(mainTheme50bpm, m_AudioSource.time);
                break;
        }
    }
}
