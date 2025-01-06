using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class CharacterController2D : MonoBehaviour
{

	[SerializeField] private LayerMask WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform GroundCheck;                             // A position marking where to check if the player is grounded.

    [SerializeField] public GameObject GameOverPanel; //Canvas for game over menu

	[Range(0, 2000f)][SerializeField] private float JumpForce = 500f;                           // Amount of force added when the player jumps.
	[Range(0, .3f)][SerializeField] private float MovementSmoothing = .05f; // How much to smooth out the movement
	[Range(.7f, 1f)][SerializeField] private float airFactor = .99f;



	public float health = 100f;
	public int damage = 50;
	public AudioClip hitClip;
	public AudioClip dieClip;
	//public GameObject redTint;



	public bool AirControl = true;
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D Rigidbody2D;
	private bool FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 stop = Vector3.zero;
	private Animator playerAnimator;
	private bool dead = false;
	private bool isAttacking;
	public bool isHit = false;
	




	void Awake()
	{
		Rigidbody2D = GetComponent<Rigidbody2D>();



		playerAnimator = GetComponent<Animator>();
		this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
		//redTint.SetActive(false);
	}

	void Update()
	{
		Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, k_GroundedRadius, WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				Grounded = true;
			}
		}
		
		if(dead){
			DieAnimation();
			dead = false;
		}
	
	}


	public void Move(float move, bool attacking, bool jump)
	{
		//only control the player if grounded or airControl is turned on
		if (Grounded || AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, Rigidbody2D.linearVelocityY);
			// And then smoothing it out and applying it to the character
			Rigidbody2D.linearVelocity = Vector3.SmoothDamp(Rigidbody2D.linearVelocity, targetVelocity, ref stop, MovementSmoothing);
			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && FacingRight)
			{
				// ... flip the player.
				Flip();
			}

		}
		// If the player should jump...
		if (Grounded && jump)
		{
			// Add a vertical force to the player.
			Grounded = false;
			Rigidbody2D.AddForce(new Vector2(0f, JumpForce));
		}
		if (!Grounded) //control in the air
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, Rigidbody2D.linearVelocity.y);
			// And then smoothing it out and applying it to the character
			Rigidbody2D.linearVelocity = Vector3.SmoothDamp(Rigidbody2D.linearVelocity, targetVelocity, ref stop, MovementSmoothing) * airFactor;

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Switches to shooting in air
			playerAnimator.SetBool("isJumping", false);
		}

	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		FacingRight = !FacingRight;
		transform.Rotate(0f, 180f,0f);
		
	}
	/* public void TakeDamage (int damage
	{
		health -= damage;
		
		HealthBarFunction.UpdateFillAmount(health);

		StartCoroutine(TakesHit());
		if (health <= 0)
		{
			dead = true;
		}
	} */
	private void DieAnimation()
	{
		Rigidbody2D.linearVelocity = stop;
		//this.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 0.5f);
		SoundManager.PlayDieSound(dieClip);
		playerAnimator.SetBool("isDead", true);
	}
	public void PlayerDie()
	{
        Time.timeScale = 0f;
        GameOverPanel.SetActive(true);
    }
	public void RestoreHealth()
	{
		this.health = 100f;
		HealthBarFunction.UpdateFillAmount(this.health);
	}
	/* private IEnumerator TakesHit()
	{
		if(!dead)
		{
			//redTint.SetActive(true);
			isHit = true;
			SoundManager.PlayHitSound(hitClip);
			this.GetComponent<BoxCollider2D>().enabled = false;
			for(int i = 0; i< 5; i++)
			{
				this.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 0.5f);
				yield return new WaitForSeconds(.2f);
				this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
				yield return new WaitForSeconds(.2f);
			}
			isHit = false;
			this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			this.GetComponent<BoxCollider2D>().enabled = true;
			redTint.SetActive(false);
		}

	} */

}