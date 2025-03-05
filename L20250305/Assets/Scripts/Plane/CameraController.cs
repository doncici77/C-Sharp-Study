using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerPos;
    public float positionLagTime = 1.0f;
    public float rotationLagTime = 1.0f;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("MyPlane").transform;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerPos.position, Time.deltaTime * positionLagTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, playerPos.rotation, Time.deltaTime * rotationLagTime);
        Debug.DrawLine(transform.position, Camera.main.transform.position, UnityEngine.Color.red);
    }
}
