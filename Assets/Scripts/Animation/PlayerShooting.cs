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
public Transform rim;
public Transform shooting_point;
public Camera camera;
float distance;

    // Update is called once per frame
    void Update()
    {
        ShootingAnimation();
    }
    private void ShootingAnimation()
    {
        if(Input.GetButton("Fire1"))
        {
            animator.SetBool("isShooting", true);
        }

    }
    private void ShootBasketball()
    {
        basketball.transform.SetParent(null);
        animator.enabled = false;

        Rigidbody rb = basketball.GetComponent<Rigidbody>();
        Ray ray = camera.ViewportPointToRay(new Vector3(0.48f,1.5f,0f));
        //distance = (float)Math.Sqrt(Mathf.Pow(rim.position.x - shooting_point.position.x, 2) + Math.Pow(rim.position.z - shooting_point.position.z, 2));
        distance = Mathf.Sqrt(Vector3.Distance(rim.position, shooting_point.position)) -0.5f;
        
        
        Vector3 targetPoint = ray.GetPoint(distance);
        Vector3 direction = targetPoint - shooting_point.position;

        if(distance < 1.2f)
        {
            distance = 1.3f;
        }

        float force = shootingPower * distance;


        rb.AddForce(direction.normalized * force, ForceMode.Impulse);
        Debug.Log(distance);

        StartCoroutine(ShootingTimer());
    }
    private IEnumerator ShootingTimer()
    {
        
        yield return new WaitForSeconds(2f);
        basketball.transform.SetParent(transform);
        animator.enabled = true;
        
    }
}
