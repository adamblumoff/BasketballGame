using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDribbling : MonoBehaviour
{
public Animator animator;
bool switches = false; 
bool isLeft; 

    // Update is called once per frame
    void Update()
    {
        DribblingAnimation();
    }
    private void DribblingAnimation()
    {
        float x = Input.GetAxis("Horizontal");
        animator.SetFloat("x", x);

        float y = Input.GetAxis("Vertical");
        animator.SetFloat("y", y);

        if(!animator.GetBool("isShooting") && x < -0.1f) //dribble left/cross right to left
        {
            animator.SetBool("isLeft", true);
            animator.SetBool("isDribbling", true);
            if(!isLeft && !animator.GetBool("isCrossover"))
            {
                animator.SetBool("isCrossover", true);
                isLeft = !isLeft;
            }
        }
        else if(!animator.GetBool("isShooting") && x > 0.1f) //dribble right/cross left to right
        {
            animator.SetBool("isLeft", false);
            animator.SetBool("isDribbling", true);
            if(isLeft && !animator.GetBool("isCrossover"))
            {
                animator.SetBool("isCrossover", true);
                isLeft = !isLeft;
            }
        }
        else if(y == 0f)
        {
            animator.SetBool("isShooting", false);
            animator.SetBool("isDribbling", false);
            animator.SetBool("isCrossover", false);
        }
    }
    private void StopCrossoverAnimation()
    {
        animator.SetBool("isCrossover", false);
    }
}
