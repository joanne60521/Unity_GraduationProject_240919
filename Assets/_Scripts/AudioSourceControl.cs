using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceControl : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.time = 0.5f;

        InvokeRepeating("PlayMiddleSection", 0f, 2f);
    }
    
    void PlayMiddleSection()
    {
        audioSource.Play();
    }

}
