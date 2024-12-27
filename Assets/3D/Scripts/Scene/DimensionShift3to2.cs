using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class DimensionShift3to2 : MonoBehaviour
{
    // Start is called before the first frame update
    public CameraShaker cameraShake;
    public Score score;
    public Volume volume;
    Vignette vignette;
    LensDistortion lensDistortion;
    ColorAdjustments colorAdjustments;
    Bloom bloom;

    void Start()
    {
        volume.profile.TryGet(out lensDistortion);
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out colorAdjustments);
        volume.profile.TryGet(out bloom);
        lensDistortion.intensity.value = 0f;
        vignette.intensity.value = 0f;
        vignette.smoothness.value = 0f;
        colorAdjustments.saturation.value = 10f;
        bloom.intensity.value = 0f;
    }
    private void StartShift()
    {
        StartCoroutine(ChangeLens());
        StartCoroutine(ChangeVignette());
        StartCoroutine(ChangeColorAdjustments());
        StartCoroutine(ChangeBloom());
        CameraShake();
    }
    private IEnumerator ChangeLens()
    {
        while(lensDistortion.intensity.value < .7f)
        {
            yield return new WaitForSeconds(.0001f);
            lensDistortion.intensity.value += .0001f;
        }
          
    }
    private IEnumerator ChangeVignette()
    {
        while(vignette.intensity.value < .9f && vignette.smoothness.value < .9f)
        {
            yield return new WaitForSeconds(.0001f);
            vignette.intensity.value += .0001f;
            vignette.smoothness.value += .0001f;
        }
    }
    private IEnumerator ChangeColorAdjustments()
    {
        while(colorAdjustments.saturation.value > -100f)
        {
            yield return new WaitForSeconds(.01f);
            colorAdjustments.saturation.value -= .01f;
        }
    }
    private IEnumerator ChangeBloom()
    {
        while(bloom.intensity.value < 3f)
        {
            yield return new WaitForSeconds(.001f);
            bloom.intensity.value += .0003f;
        }
        ChangeScene();
    }
    private void ChangeScene()
    {
        SceneManager.LoadScene("2DGameScene");
    }
    private void CameraShake()
    {
        cameraShake.ShakeOnce(4f, 1.5f, 2f, 1f);
    }
    // Update is called once per frame
    void Update()
    {
        if(Score.score == 2)
        {
            StartShift();
        }
        
    }
}
