using UnityEngine;

public class TargetPointController : MonoBehaviour
{
    public Transform player;
    public Transform backboard;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((player.position.x + backboard.position.x)/2, 0f, 0f);
    }
}
