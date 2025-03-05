using UnityEngine;

public class LerpSample : MonoBehaviour
{
    public Transform A;
    public Transform B;
    public Material material;

    [Range (0, 1)]
    public float T = 0;

    float elapsedTime = 0;
    float sign = 1;

    public AnimationCurve curve;

    void Update()
    {
        elapsedTime += sign * Time.deltaTime;

        if(elapsedTime > 1 || elapsedTime < 0)
        {
            sign = sign * -1;
        }

        transform.position = Vector3.Lerp(A.position, B.position, curve.Evaluate(elapsedTime));
        transform.rotation = Quaternion.Slerp(A.rotation, B.rotation, curve.Evaluate(elapsedTime));

        material.color = Color.Lerp(Color.red, Color.blue, curve.Evaluate(elapsedTime));
    }
}
