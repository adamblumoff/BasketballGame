using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.MPE;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    private float gravity = -6f;
    public float jumpHeight;
    
    public Transform groundCheck;
    private float groundDistance = 0.1f;
    public LayerMask groundMask;
    bool isGrounded;
    Vector3 velocity;
    public Animator animator;
    bool isJumping = false;
    public bool isSprinting = false;
    private bool isTired = false;
    public float sprintNum = 100f;

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }
    void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            
        } 
        
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime); 
    }
    void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }   

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        
    
        Sprint();
        SprintBarUpdate();
        
        controller.Move(move * speed * Time.deltaTime);
    }
    public void ChangeJumpStatus()
    {
        isJumping = !isJumping;
    }
    void Sprint()
    {
        if(Input.GetButton("Fire3"))
        {
            if(!isTired)
            {
                speed = 3f;
                isSprinting = true;
            } 
            else
            {
                speed = 1f;
                isSprinting = true;
            }
        }
        else{
            if(!isTired)
            {
                speed = 2.4f;
                isSprinting = false;
            } 
            else
            {
                speed = 1.2f;
                isSprinting = false;
            }
        }
        
    }
    void SprintBarUpdate()
    {
        if(isSprinting && sprintNum > 0f)
        {
            sprintNum-=0.5f;
            SprintBarFunction.SetSprint(sprintNum);
        }
        else if(isSprinting && sprintNum <= 0f)
        {
            SprintBarFunction.SetSprint(sprintNum);
            isTired = true;
        }
        else if (!isSprinting && sprintNum <= 99.5f)
        {
            sprintNum+=0.5f;
            SprintBarFunction.SetSprint(sprintNum);
        }
        else if(sprintNum == 100f)
        {
            isTired = false;
        }
    }

}
