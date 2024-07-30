using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public Score score;

    // Update is called once per frame
    
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.CompareTag("Basketball"))
        {
            score.isTrigger = true;
        }
    }
}
