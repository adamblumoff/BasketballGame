using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Reference to the AudioSource component
    private static AudioSource audioSource;


    // Awake is called when the script instance is being loaded
    void Awake()
    {
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // This method plays a given audio clip
    public static void PlayZombieGrunt(AudioClip gruntClip)
    {
        if (gruntClip != null)
        {
            audioSource.PlayOneShot(gruntClip);
        }
        else
        {
            Debug.LogWarning("AudioClip is null");
        }
    }

    public static void PlayZombieHurt(AudioClip hurtClip)
    {
        if (hurtClip != null)
        {
            audioSource.PlayOneShot(hurtClip);
        }
        else
        {
            Debug.LogWarning("AudioClip is null");
        }
    }

    public static void PlayBarrelExplosion(AudioClip explosionClip)
    {
        if (explosionClip != null)
        {
            audioSource.PlayOneShot(explosionClip, .5f);
        }
        else
        {
            Debug.LogWarning("AudioClip is null");
        }
    }
    public static void PlayMovementSound(AudioClip moveClip)
    {
        if (moveClip != null)
        {
            audioSource.PlayOneShot(moveClip);
        }
        else
        {
            Debug.LogWarning("AudioClip is null");
        }
    }
    public static void PlayAttackSound(AudioClip attackClip)
    {
        if (attackClip != null)
        {
            audioSource.PlayOneShot(attackClip);
        }
        else
        {
            Debug.LogWarning("AudioClip is null");
        }
    }
    public static void PlayHitSound(AudioClip hitClip)
    {
        if (hitClip != null)
        {
            audioSource.PlayOneShot(hitClip);
        }
        else
        {
            Debug.LogWarning("AudioClip is null");
        }
    }
    public static void PlayDieSound(AudioClip dieClip)
    {
        if (dieClip != null)
        {
            audioSource.PlayOneShot(dieClip);
        }
        else
        {
            Debug.LogWarning("AudioClip is null");
        }
    }
    public static void PlayUpgradeSound(AudioClip dieClip)
    {
        if (dieClip != null)
        {
            audioSource.PlayOneShot(dieClip);
        }
        else
        {
            Debug.LogWarning("AudioClip is null");
        }
    }
    public static void PlayRockSound(AudioClip rockClip)
    {
        if (rockClip != null)
        {
            audioSource.PlayOneShot(rockClip);
        }
        else
        {
            Debug.LogWarning("AudioClip is null");
        }
    }
    public static void PlayEarthQuakeSound(AudioClip earthquakeClip)
    {
        if (earthquakeClip != null)
        {
            audioSource.PlayOneShot(earthquakeClip);
        }
        else
        {
            Debug.LogWarning("AudioClip is null");
        }
    }

}
