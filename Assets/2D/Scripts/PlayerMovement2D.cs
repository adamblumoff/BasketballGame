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
            JumpingAnimation();
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
        if (isHorizontal)
            controller.Move(horizontalMove * Time.fixedDeltaTime, isHorizontal, Shooting, jump);
        else
            controller.Move(verticalMove * Time.fixedDeltaTime, isHorizontal, Shooting, jump);

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
        verticalMove = Input.GetAxisRaw("Vertical2D") * runSpeed;
        

        if (horizontalMove > 0)
        {
            isHorizontal = true;
            animator.SetFloat("Horizontal2D", Math.Abs(horizontalMove));
            animator.SetFloat("Vertical2D", 0f);
        }
        else if (horizontalMove < 0)
        {   
            isHorizontal = true;
            animator.SetFloat("Horizontal2D", Math.Abs(horizontalMove));
            animator.SetFloat("Vertical2D", 0f);
        }
        else if (verticalMove > 0)
        {
            isHorizontal = false;
            isDown = false;
            animator.SetFloat("Vertical2D", Math.Abs(verticalMove));
            animator.SetFloat("Horizontal2D", 0f);
        }
        else if (verticalMove < 0)
        {
            isHorizontal = false;
            isDown = true;
            animator.SetFloat("Vertical2D", Math.Abs(verticalMove));
            animator.SetFloat("Horizontal2D", 0f);
        }
        else
        {
            animator.SetFloat("Vertical2D", 0f);
            animator.SetFloat("Horizontal2D", 0f);
        }
        MovementAnimation();

    }
    private void ShootingAnimation() //sets rapid fire and Shooting animations
    {
        if (Input.GetButtonDown("Fire1") && !character.isHit)
        {
            Shooting = true;
            animator.SetBool("isShooting", true);
        }

    }
    private void MovementAnimation() //sets movement in animator
    {
        if (isHorizontal)
            animator.SetBool("isHorizontal", true);
        else
            animator.SetBool("isHorizontal", false);

        if (isDown)
            animator.SetBool("isDown", true);
        else
            animator.SetBool("isDown", false);
    }
    private void JumpingAnimation()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

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
