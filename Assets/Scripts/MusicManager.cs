using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    public AudioClip titleMusic;
    public AudioClip introMusic;

    AudioSource m_AudioSource;

    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
            return;
        } else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
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

    void PlayTrack(AudioClip clip)
    {
        m_AudioSource.volume = 1f;
        m_AudioSource.clip = clip;
        m_AudioSource.Play();
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
            default:
                PlayTrack(introMusic);
                break;
        }
    }
}
