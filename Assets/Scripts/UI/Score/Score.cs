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
    public GameObject is2PointGameObject;
    TwoPoint isTwoPoint;
    public GameObject TwoPointFloatingScore;
    Animator TwoPointFloatingAnimator;
    public GameObject ThreePointFloatingScore;
    Animator ThreePointFloatingAnimator;
    public AudioClip swish;
    public bool isTrigger;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreCountText = scoreCount.GetComponent<TextMeshProUGUI>();
        isTwoPoint = is2PointGameObject.GetComponent<TwoPoint>();
        TwoPointFloatingAnimator = TwoPointFloatingScore.GetComponent<Animator>();
        ThreePointFloatingAnimator = ThreePointFloatingScore.GetComponent<Animator>();
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.CompareTag("Basketball") && isTwoPoint.is2Point && isTrigger)
        {
            AudioManager.PlayAudioClip(swish, .6f);
            AddScore2();
            StartCoroutine(TwoPointFloatAnimation());
            isTrigger = false;
        }
        else if(other.gameObject.CompareTag("Basketball") && !isTwoPoint.is2Point && isTrigger)
        {
            AudioManager.PlayAudioClip(swish, .6f);
            AddScore3();
            StartCoroutine(ThreePointFloatAnimation());
            isTrigger = false;
        }
        else
        {
            isTrigger = false;
        }
    }
    private static void AddScore2()
    {
        score +=2;
        scoreCountText.text = score.ToString();
    }
    private static void AddScore3()
    {
        score +=3;
        scoreCountText.text = score.ToString();
    }
    static int GetScore()
    {
        return score;
    }
    private IEnumerator TwoPointFloatAnimation()
    {
        TwoPointFloatingScore.SetActive(true);
        TwoPointFloatingAnimator.SetBool("isScoring", true);
        yield return new WaitForSeconds(2f);
        TwoPointFloatingScore.SetActive(false);
        TwoPointFloatingAnimator.SetBool("isScoring", false);
        
    }
    private IEnumerator ThreePointFloatAnimation()
    {
        ThreePointFloatingScore.SetActive(true);
        ThreePointFloatingAnimator.SetBool("isScoring", true);
        yield return new WaitForSeconds(2f);
        ThreePointFloatingScore.SetActive(false);
        ThreePointFloatingAnimator.SetBool("isScoring", false);
        
    }

}
