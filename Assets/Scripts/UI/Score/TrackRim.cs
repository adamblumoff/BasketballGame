using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackRim : MonoBehaviour
{
    public Gradient gradient;
    public Image dot;
    public Transform rim;
    public Camera camera;
    public float y;
    void Start()
    {
        dot.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f,y,0f));
        if(Vector3.Distance(ray.direction, rim.position) < 5f)
        {
            dot.color = gradient.Evaluate(1f);
        }
        else if(Vector3.Distance(ray.direction, rim.position) < 5.2f)
        {
            dot.color = gradient.Evaluate(.5f);
        }
        else
        {
            dot.color = gradient.Evaluate(0f);
        }
        Debug.Log(Vector3.Distance(ray.direction, rim.position));
        Debug.DrawLine(ray.origin, ray.GetPoint(100f));
    }
}
