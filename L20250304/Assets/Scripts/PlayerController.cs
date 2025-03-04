using System.Linq;
using UnityEngine;

// 입력이나 다른 이벤트를 처리한다.
// 현재 게임오브젝트에 다른 컴포넌트에 명령을 내린다.
// 사용자 컴포넌트는 한가지 일만 해야한다.
public class PlayerController : MonoBehaviour
{
    MeshRenderer meshRenderer;

    float moveSpeed = 3.0f;
    float rotationSpeed = 60.0f;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        // velocity => vector 크기랑 방향
        // s = v * t // speed * direction * time

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // 로컬 좌표계, 월드 좌표계
        // 세상의 위방향
        //transform.position += transform.up * v * Time.deltaTime * moveSpeed;
        transform.Translate(Vector3.up * v * Time.deltaTime * moveSpeed, Space.Self);
        transform.eulerAngles += transform.forward * -h * Time.deltaTime * rotationSpeed;


    }
}
