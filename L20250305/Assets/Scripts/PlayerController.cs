using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float rotateSpeed = 60.0f;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.forward * -h * rotateSpeed * Time.deltaTime, Space.Self);
    }
}
