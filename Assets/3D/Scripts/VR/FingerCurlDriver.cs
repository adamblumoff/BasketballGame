using UnityEngine;
using UnityEngine.InputSystem;

public class FingerCurlDriver : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference trigger;   // XRI LeftHand/Trigger
    [SerializeField] private InputActionReference grip;      // XRI LeftHand/Grip

    [Header("Bones")]
    [SerializeField] private Transform[] indexJoints;        // size = 3 (Index1-3)
    [SerializeField] private Transform[] middleJoints;       // size = 3 (Middle1-3)

    [Header("Angles")]
    [SerializeField, Range(0, 90)] private float curlAngle = 70f;

    private void OnEnable()
    {
        trigger.action.Enable();
        grip.action.Enable();
    }
    private void OnDisable()
    {
        trigger.action.Disable();
        grip.action.Disable();
    }

    void Update()
    {
        float index = trigger.action.ReadValue<float>();  // 0⟶1
        float other = grip.action.ReadValue<float>();  // 0⟶1
        ApplyCurl(indexJoints, index);
        ApplyCurl(middleJoints, other);
    }
    void ApplyCurl(Transform[] chain, float t)
    {
        Quaternion rot = Quaternion.Euler(curlAngle * t, 0, 0);
        foreach (var j in chain) j.localRotation = rot;
    }
}
