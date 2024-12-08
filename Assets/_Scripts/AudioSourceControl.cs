using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceControl : MonoBehaviour
{
    public AudioSource[] audioSources;
    public StartGameControl startGameControl;
    private bool b = true;

    void Start()
    {
        // audioSources = GetComponents<AudioSource>();
        // audioSources[0] = GetComponent<AudioSource>();

        // audioSources[0].time = 0.5f;

        // InvokeRepeating("PlayMiddleSection", 0f, 2f);
    }
    
    void Update()
    {
        if(startGameControl.startGame && b)
        {
            b = false;
            audioSources = GetComponents<AudioSource>();
            // audioSources[0] = GetComponent<AudioSource>();
            audioSources[1].enabled = true;

            audioSources[0].time = 0.5f;

            InvokeRepeating("PlayMiddleSection", 0f, 2f);
        }
    }
    
    void PlayMiddleSection()
    {
        audioSources[0].Play();
    }

}
