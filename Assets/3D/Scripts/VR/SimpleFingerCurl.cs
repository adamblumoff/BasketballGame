using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleFingerCurl : MonoBehaviour
{
    public Transform index1, index2, index3;   // drag bones
    public InputAction trigger;

    [Tooltip("Degrees the finger closes when trigger = 1")]
    public float curlAngle = 70f;

    void Update()
    {
        float t = trigger.ReadValue<float>();   // 0-1
        Quaternion rot = Quaternion.Euler(curlAngle * t, 0, 0);
        index1.localRotation = rot;
        index2.localRotation = rot;
        index3.localRotation = rot;
    }
}