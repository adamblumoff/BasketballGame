using UnityEngine;
using UnityEngine.InputSystem;

public class DumpXRDevices : MonoBehaviour
{
    void Start()
    {
        foreach (var d in InputSystem.devices)
            Debug.Log($"DEVICE: {d}  usages={string.Join(",", d.usages)}");
    }
}
