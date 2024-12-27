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
    public GameObject streakCount;
    public static TextMeshProUGUI streakCountText;
    public GameObject is2PointGameObject;
    TwoPoint isTwoPoint;
    public GameObject TwoPointFloatingScore;
    Animator TwoPointFloatingAnimator;
    public GameObject ThreePointFloatingScore;
    Animator ThreePointFloatingAnimator;
    public GameObject StreakFloatingScore;
    Animator StreakTextFloatingAnimator;
    Animator StreakCountFloatingAnimator;
    public AudioClip swish;
    public bool isTrigger;
    public static int streak;
    public static bool isMake;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        streak = 0;
        scoreCountText = scoreCount.GetComponent<TextMeshProUGUI>();
        streakCountText = streakCount.GetComponent<TextMeshProUGUI>();
        isTwoPoint = is2PointGameObject.GetComponent<TwoPoint>();
        TwoPointFloatingAnimator = TwoPointFloatingScore.GetComponent<Animator>();
        ThreePointFloatingAnimator = ThreePointFloatingScore.GetComponent<Animator>();
        StreakTextFloatingAnimator = StreakFloatingScore.GetComponent<Animator>();
        StreakCountFloatingAnimator = streakCount.GetComponent<Animator>();
        
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.CompareTag("Basketball") && isTwoPoint.is2Point && isTrigger)
        {
            AudioManager.PlayAudioClip(swish, .6f);
            AddScore2();
            isMake = true;
            StartCoroutine(StreakFloatAnimation());
            StartCoroutine(TwoPointFloatAnimation());
            isTrigger = false;
            
        }
        else if(other.gameObject.CompareTag("Basketball") && !isTwoPoint.is2Point && isTrigger)
        {
            AudioManager.PlayAudioClip(swish, .6f);
            AddScore3();
            isMake = true;
            StartCoroutine(StreakFloatAnimation());
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
    private IEnumerator TwoPointFloatAnimation()
    {
        AddToStreak();
        TwoPointFloatingScore.SetActive(true);
        TwoPointFloatingAnimator.SetBool("isActive", true);
        yield return new WaitForSeconds(2f);
        TwoPointFloatingScore.SetActive(false);
        TwoPointFloatingAnimator.SetBool("isActive", false);
        isMake = false;
        
    }
    private IEnumerator ThreePointFloatAnimation()
    {
        AddToStreak();
        ThreePointFloatingScore.SetActive(true);
        ThreePointFloatingAnimator.SetBool("isActive", true);
        yield return new WaitForSeconds(2f);
        ThreePointFloatingScore.SetActive(false);
        ThreePointFloatingAnimator.SetBool("isActive", false);
        isMake = false;
        
    }
    private IEnumerator StreakFloatAnimation()
    {
        StreakFloatingScore.SetActive(true);
        StreakTextFloatingAnimator.SetBool("isActive", true);
        streakCount.SetActive(true);
        StreakCountFloatingAnimator.SetBool("isActive", true);
        yield return new WaitForSeconds(2f);
        StreakFloatingScore.SetActive(false);
        StreakTextFloatingAnimator.SetBool("isActive", false);
        streakCount.SetActive(false);
        StreakCountFloatingAnimator.SetBool("isActive", false);
    }
    private static void AddToStreak()
    {
        if(isMake)
        {
            Score.streak++;
        }
        streakCountText.text = streak.ToString();
    }
}
