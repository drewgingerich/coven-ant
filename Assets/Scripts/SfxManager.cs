using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
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
        PlayOneShot(arrowClick);
    }

    public void PlayCameraShutter()
    {
        PlayOneShot(cameraShutter);
    }

    public void PlayCandleOut()
    {
        PlayOneShot(candleOut);
    }

    public void PlayItemSwitch()
    {
        PlayOneShot(itemSwitch);
    }

    public void PlayPoof()
    {
        PlayOneShot(poof);
    }

    public void PlayStart()
    {
        PlayOneShot(start1);
    }

    void PlayRandom(List<AudioClip> clips)
    {
        PlayOneShot(clips[Random.Range(0, clips.Count)]);
    }

    void PlayOneShot(AudioClip clip)
    {
        if(m_Audiosource == null)
        {
            m_Audiosource = GetComponent<AudioSource>();
            if(m_Audiosource == null)
            {
                Debug.LogError("Couldn't get AudioSource on SfxManager");
                return;
            }
        }
        m_Audiosource.PlayOneShot(clip);
    }
}
