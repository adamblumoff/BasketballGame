using UnityEngine;
using UnityEngine.UI;
public class SprintBarFunction : MonoBehaviour
{
    public static Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 100f;
    }
    public static void SetSprint(float sprintNum)
    {
        slider.value = sprintNum;
    }
}
