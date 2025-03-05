using UnityEngine;

public class ChracterMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float rotationSpeed = 360f;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.forward * v * Time.deltaTime * moveSpeed);
        transform.Rotate(Vector3.up * h * Time.deltaTime * rotationSpeed);
    }
}
