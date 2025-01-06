using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    private CharacterController2D character;
    public float runSpeed = 40f;
    public AudioClip moveClip;
    

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool isHorizontal = false;
    bool isDown = true;
    public bool Shooting = false;
    private bool isSoundPlaying = false;
    bool jump = false;


    // Update is called once per frame
    void Start()
    {
        character = FindFirstObjectByType<CharacterController2D>();
    }
    void Update()
    {
        if (!animator.GetBool("isDead")) // moves and attacks when not already Shooting or dead, also sets rapid fire
        {
            Movement();
            MovementSound();
            Jump();
        }
        else
        {
            horizontalMove = 0f;
            verticalMove = 0f;
        }


    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, Shooting, jump);
    

    }

    private void MovementSound() //sets movement sound
    {
        if((verticalMove > 0 || horizontalMove > 0) && !isSoundPlaying)
            {
                //SoundManager.PlayMovementSound(moveClip);
                isSoundPlaying = true;
                //StartCoroutine(PlayMoveSound());
            }
    }
    private void Movement() //conditions for movement and sets animator
    {
        horizontalMove = Input.GetAxisRaw("Horizontal2D") * runSpeed;
    

        if (horizontalMove > 0)
        {
            animator.SetFloat("Horizontal2D", Math.Abs(horizontalMove));
        }
        else if (horizontalMove < 0)
        {   
            animator.SetFloat("Horizontal2D", Math.Abs(horizontalMove));
        }
        else
        {
            animator.SetFloat("Horizontal2D", 0f);
        }

    }
    private void ShootingAnimation() //sets rapid fire and Shooting animations
    {
        if (Input.GetButtonDown("Fire1") && !character.isHit)
        {
            Shooting = true;
            animator.SetBool("isShooting", true);
        }

    }

    private void Jump()
    {
        if(Input.GetButton("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }
        else
        {
            StopJumping();
        }
        
    }
    private void StopJumping()
    {
        jump = false;
        animator.SetBool("isJumping", false);
    }
    public void StopShooting() //gets called in animator as an animation event
    {
        Shooting = false;
        animator.SetBool("isShooting", false);
    }

    public void UpgradeSpeed() //upgrades speed and caps at 40
    {
        if(this.runSpeed <=40)
            this.runSpeed *= 1.1f;
    }
    private IEnumerator PlayMoveSound()
    {
        yield return new WaitForSeconds(moveClip.length);
        isSoundPlaying = false;
    }


}
