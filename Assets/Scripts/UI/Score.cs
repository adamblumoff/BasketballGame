using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public GameObject scoreCount;
    public static int score;
    private static TextMeshProUGUI scoreCountText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreCountText = scoreCount.GetComponent<TextMeshProUGUI>();
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.CompareTag("Basketball"))
        {
            AddScore();
        }
    }
    private static void AddScore()
    {
        score +=2;
        scoreCountText.text = score.ToString();
    }
    static int GetScore()
    {
        return score;
    }
}
