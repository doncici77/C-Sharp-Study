using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 5.0f;

    void Awake()
    {
        Destroy(gameObject, 3.0f);
    }

    void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
    }
}
