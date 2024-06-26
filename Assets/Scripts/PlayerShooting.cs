using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
public Animator animator;

    // Update is called once per frame
    void Update()
    {
        ShootingAnimation();
    }
    private void ShootingAnimation()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("isShooting", true);
        }
    }
    private void StopShootingAnimation()
    {
        animator.SetBool("isShooting", false);
    }
}
