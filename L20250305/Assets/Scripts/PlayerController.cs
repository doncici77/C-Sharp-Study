using NUnit.Framework;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float rotateSpeed = 60.0f;

    public GameObject bulletPrefab;
    public Transform[] bulletSpawnPos;

    private void Awake()
    {
        bulletSpawnPos = GameObject.Find("BulletSpawn").GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.forward * -h * rotateSpeed * Time.deltaTime, Space.Self);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet;
            for (int i = 0; i < bulletSpawnPos.Length; i++)
            {
                bullet = Instantiate(bulletPrefab);
                bullet.transform.position = bulletSpawnPos[i].position;
                bullet.transform.rotation = bulletSpawnPos[i].rotation;
            }
        }
    }
}
