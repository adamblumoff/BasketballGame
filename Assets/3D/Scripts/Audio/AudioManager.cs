using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioSource audioSource;
    


    // Awake is called when the script instance is being loaded
    void Awake()
    {
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f;
            audioSource.maxDistance = 20f;
        } 
    }

    // Update is called once per frame
    public static void PlayAudioClip(AudioClip audioClip, float intensity)
    {
        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip, intensity);
        }
        else
        {
            Debug.LogWarning("AudioClip is null");
        }
    }
}
