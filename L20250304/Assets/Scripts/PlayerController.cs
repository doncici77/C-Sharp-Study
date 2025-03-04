using System.Linq;
using UnityEngine;

// �Է��̳� �ٸ� �̺�Ʈ�� ó���Ѵ�.
// ���� ���ӿ�����Ʈ�� �ٸ� ������Ʈ�� ����� ������.
// ����� ������Ʈ�� �Ѱ��� �ϸ� �ؾ��Ѵ�.
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
        // velocity => vector ũ��� ����
        // s = v * t // speed * direction * time

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // ���� ��ǥ��, ���� ��ǥ��
        // ������ ������
        //transform.position += transform.up * v * Time.deltaTime * moveSpeed;
        transform.Translate(Vector3.up * v * Time.deltaTime * moveSpeed, Space.Self);
        transform.eulerAngles += transform.forward * -h * Time.deltaTime * rotationSpeed;


    }
}
