using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

    void PlayTrack(AudioClip clip)
    {
        // TODO: use DOTween to mimic crossfade
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
                break;
        }
    }
}
