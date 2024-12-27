using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabbing : MonoBehaviour
{
public Animator animator;
bool switches = false; 
bool isLeft; 

    // Update is called once per frame
    void Update()
    {
        //GrabbingAnimation();
    }
    private void GrabbingAnimation()
    {
        if(Input.GetButton("Fire2"))
        {
            animator.SetBool("isGrabbing", true);
        }
        else 
        {
            animator.SetBool("isGrabbing", false);
        }
    }
}
