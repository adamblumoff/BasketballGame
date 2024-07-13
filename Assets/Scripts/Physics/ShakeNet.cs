using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ShakeNet : MonoBehaviour
{
    

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
         if(other.gameObject.CompareTag("Basketball"))
         {
            Debug.Log("shake");
            this.GetComponent<Rigidbody>().isKinematic = false;
         }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Basketball"))
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
