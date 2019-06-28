using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public List<AudioClip> witchLaughs;

    AudioSource m_Audiosource;

    // Start is called before the first frame update
    void Start()
    {
        m_Audiosource = GetComponent<AudioSource>();
    }

    public void PlayWitchLaugh()
    {
        m_Audiosource.PlayOneShot(witchLaughs[Random.Range(0, witchLaughs.Count)]);
    }
}
