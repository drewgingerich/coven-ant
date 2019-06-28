using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public List<AudioClip> witchLaughs;
    public List<AudioClip> antScreams;
    public List<AudioClip> antConcerned;
    public AudioClip antGasp;
    public AudioClip antNervousLaugh;
    public AudioClip antSigh;

    AudioSource m_Audiosource;

    // Start is called before the first frame update
    void Start()
    {
        m_Audiosource = GetComponent<AudioSource>();
    }

    public void PlayWitchLaugh()
    {
        PlayRandom(witchLaughs);
    }

    public void PlayAntScream()
    {
        PlayRandom(antScreams);
    }

    public void PlayAntConcerned()
    {
        PlayRandom(antConcerned);
    }

    public void PlayAntGasp()
    {
        m_Audiosource.PlayOneShot(antGasp);
    }

    public void PlayAntNervousLaugh()
    {
        m_Audiosource.PlayOneShot(antNervousLaugh);
    }

    public void PlayAntSigh()
    {
        m_Audiosource.PlayOneShot(antSigh);
    }

    void PlayRandom(List<AudioClip> clips)
    {
        m_Audiosource.PlayOneShot(clips[Random.Range(0, clips.Count)]);
    }
}
