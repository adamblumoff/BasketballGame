using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDunking : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public PlayerMovement playerMovement;
    public bool inDunkingZone;
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            inDunkingZone = true;
        }
    }
    void OnTriggerExit (Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            inDunkingZone = false;
            
        }
    }
     void Update()
    {
        if(!playerMovement.isGrounded && inDunkingZone)
        {
            animator.SetBool("isDunking", true);
            
        }
        else if(inDunkingZone)
        {
            playerMovement.jumpHeight = 1.6f;
        }
        else
        {
            animator.SetBool("isDunking", false);
            animator.SetBool("isShooting", false);
            playerMovement.jumpHeight = .8f;
        }
    } 
}
