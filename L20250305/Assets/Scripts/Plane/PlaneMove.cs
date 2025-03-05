using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    public float Speed = 5.0f;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }
}
