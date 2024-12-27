using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
public Animator animator;
public float shootingPower;
public GameObject basketball;
public Transform parent;
public Transform end_position;
public Transform ReleasePosition;
public Camera Camera;
public LineRenderer LineRenderer;
public BasketballController basketball_controller;
public PauseMenu pauseMenu;
private Rigidbody rb;
private PlayerMovement player_mov;
float distance;
[SerializeField]
[Range(10, 100)]
private int LinePoints = 25;
[SerializeField]
[Range(0.01f, 0.25f)]
private float TimeBetweenPoints = 0.1f;

private LayerMask BasketballCollisionMask;
private Vector3 direction;
private Vector3 targetPoint;
private Ray ray;

    // Update is called once per frame
    void Awake()
    {
        int basketballLayer = basketball.gameObject.layer;
        for (int i = 0; i < 32; i++)
        {
            if (!Physics.GetIgnoreLayerCollision(basketballLayer, i))
            {
                BasketballCollisionMask |= 1 << i; 
            }
        }
        rb = basketball.GetComponent<Rigidbody>();
        player_mov = GetComponentInParent<PlayerMovement>();
    }
    void Update()
    {
        ShootingAnimation();
    }
    private void ShootingAnimation()
    {
        if(Input.GetButton("Fire1"))
        {
            animator.SetBool("isShooting", true);
            if(Input.GetButton("Fire2"))
            {
                DrawProjection();
            }
        }
        else if(Input.GetButton("Fire2"))
        {
            DrawProjection();
        }
        else
        {
            LineRenderer.enabled = false;
        }

        if(animator.GetBool("isShooting") && !animator.GetBool("isDunking"))
        {
            LockLocation();
        }
        else
        {
            UnLockLocation();
        }
        

    }
    private void ShootBasketball()
    {
        animator.enabled = false;
        
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = false;

        rb.transform.SetParent(null);
        rb.AddForce(GetDirection() * shootingPower, ForceMode.Impulse);


        StartCoroutine(ShootingTimer());
    }
    private IEnumerator ShootingTimer()
    {
        yield return new WaitForSeconds(3f);
        animator.SetBool("isShooting", false);
        animator.SetBool("isDribbling", false);
        animator.SetBool("isCrossover", false);
        basketball.transform.SetParent(transform);
        animator.enabled = true;
        player_mov.isShooting = false;
        
    }
     private void DrawProjection()
    {
        LineRenderer.enabled = true;
        LineRenderer.positionCount = Mathf.CeilToInt(LinePoints / TimeBetweenPoints) + 1;
        
        Vector3 direction = GetDirection();

        Vector3 startPosition = ReleasePosition.position + new Vector3(0f, .5f, 0f);
        Vector3 startVelocity = shootingPower * direction / rb.mass;
        int i = 0;
        LineRenderer.SetPosition(i, startPosition);
        for (float time = 0; time < LinePoints; time += TimeBetweenPoints)
        {
            i++;
            Vector3 point = startPosition + time * startVelocity;
            point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);

            LineRenderer.SetPosition(i, point);

            Vector3 lastPosition = LineRenderer.GetPosition(i - 1);

            if (Physics.Raycast(lastPosition, 
                (point - lastPosition).normalized, 
                out RaycastHit hit,
                (point - lastPosition).magnitude,
                BasketballCollisionMask))
            {
                LineRenderer.SetPosition(i, hit.point);
                LineRenderer.positionCount = i + 1;
                return;
            } 
        }
        
    }
    private Vector3 GetDirection()
    {
        
        ray = Camera.ViewportPointToRay(new Vector3(0.5f,1.25f,0f));
        distance = Mathf.Sqrt(Vector3.Distance(end_position.position, ReleasePosition.position));
        
        if(distance <= 2.75f)
        {
            rb.mass = .36f;
            shootingPower = 1f;
        }
        else if(distance <= 1.75f)
        {
            rb.mass = .38f;
            shootingPower = 1.2f;
        }
        else
        {
            rb.mass = .35f;
            shootingPower = 1.0f;
        }
    
        targetPoint = ray.GetPoint(distance);
        direction = targetPoint - ReleasePosition.position + new Vector3(0f, 1f, 0f);;
        return direction;

    }
    public float GetDistance()
    {
        return distance;
    }
    private void ReturnToParent(Collider other)
    {
        if(other.gameObject.CompareTag("Court"))
        {
            StartCoroutine(ShootingTimer());
        }
    }
    private void LockLocation()
    {
        if(!pauseMenu.paused)
        {
            player_mov.isShooting = true;
        }
        
    }
    private void UnLockLocation()
    {
        player_mov.isShooting = false;
        
    }
    void DunkBasketball()
    {
        animator.enabled = false;
        player_mov.isShooting = false;
        
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = false;

        rb.transform.SetParent(null);
        rb.AddForce(-Vector3.up, ForceMode.Impulse);
        animator.SetBool("isDunking", false);

    

        StartCoroutine(DunkingTimer());
    }
    IEnumerator DunkingTimer()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("isShooting", false);
        animator.SetBool("isDribbling", false);
        animator.SetBool("isCrossover", false);
        basketball.transform.SetParent(transform);
        animator.enabled = true;
    }
    
}
