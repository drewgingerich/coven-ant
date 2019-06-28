using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager Instance { get; private set; }

    public List<AudioClip> witchLaughs;
    public List<AudioClip> antSounds;

    public AudioClip arrowClick;
    public AudioClip cameraShutter;
    public AudioClip candleOut;
    public AudioClip itemSwitch;
    public AudioClip poof;
    public AudioClip start1;
    public AudioClip start2;

    AudioSource m_Audiosource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
        //DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        m_Audiosource = GetComponent<AudioSource>();
    }

    public void PlayWitchLaugh()
    {
        PlayRandom(witchLaughs);
    }

    public void PlayAntSound()
    {
        PlayRandom(antSounds);
    }

    public void PlayArrowClick()
    {
        m_Audiosource.PlayOneShot(arrowClick);
    }

    public void PlayCameraShutter()
    {
        m_Audiosource.PlayOneShot(cameraShutter);
    }

    public void PlayCandleOut()
    {
        m_Audiosource.PlayOneShot(candleOut);
    }

    public void PlayItemSwitch()
    {
        m_Audiosource.PlayOneShot(itemSwitch);
    }

    public void PlayPoof()
    {
        m_Audiosource.PlayOneShot(poof);
    }

    public void PlayStart()
    {
        m_Audiosource.PlayOneShot(start1);
    }

    void PlayRandom(List<AudioClip> clips)
    {
        m_Audiosource.PlayOneShot(clips[Random.Range(0, clips.Count)]);
    }
}
