using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        transform.position = player.position;
    }
}
