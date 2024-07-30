using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TwoPoint : MonoBehaviour
{
    public bool is2Point;
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            is2Point = true;
        }
    }
    void OnTriggerExit (Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            is2Point = false;
        }
    }
}
