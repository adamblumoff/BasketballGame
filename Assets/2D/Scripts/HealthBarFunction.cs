using UnityEngine;
using UnityEngine.UI;

public class HealthBarFunction : MonoBehaviour
{
    public static Image fillImage;
    public static float maxHealth = 100f;
    public Image fillImageTemp;

    private void Start()
    {
        fillImage = fillImageTemp; //Assigning static variable
    }
    public static void UpdateFillAmount(float currentHealth) //Updating healthbar after player takes damage or heals
    {
        if (fillImage != null)
            fillImage.fillAmount = currentHealth / maxHealth;
    }
}
