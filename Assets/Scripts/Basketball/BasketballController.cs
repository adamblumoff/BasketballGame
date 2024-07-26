using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasketballController : MonoBehaviour
{
    public AudioClip bouncing;
    public AudioClip backboard;
     public AudioClip rim;
    void Start()
    {
    }

    void OnCollisionEnter (Collision collision)
    {
        if(collision.gameObject.CompareTag("Court") && this.gameObject.transform.parent == null)
        {
            AudioManager.PlayAudioClip(bouncing, 1f);
        }
        else if(collision.gameObject.CompareTag("Backboard") && this.gameObject.transform.parent == null)
        {
            AudioManager.PlayAudioClip(backboard, 1f);
        }
        else if(collision.gameObject.CompareTag("Rim") && this.gameObject.transform.parent == null)
        {
            AudioManager.PlayAudioClip(rim, 1f);
        }
    }
}
