using UnityEngine;

public class PlaneRotator : MonoBehaviour
{
    public float rollingSpeed = 60.0f;
    public float pitchingSpeed = 50.0f;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.Rotate(Vector3.forward * -h * Time.deltaTime * rollingSpeed);
        transform.Rotate(Vector3.right * v * Time.deltaTime * pitchingSpeed);

    }
}
