using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip background;
    private static AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.clip = background;
            audioSource.volume = .15f;
            audioSource.priority = 228;
            audioSource.Play();
        }
    }

    
}
