using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager Instance { get; private set; }

    public List<AudioClip> witchLaughs;
    public List<AudioClip> antSounds;

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

    void PlayRandom(List<AudioClip> clips)
    {
        m_Audiosource.PlayOneShot(clips[Random.Range(0, clips.Count)]);
    }
}
