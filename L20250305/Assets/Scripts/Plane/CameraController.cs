using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerPos;
    public float positionLagTime = 1.0f;
    public float rotationLagTime = 1.0f;

    Vector3 currnetVelocity;
    Quaternion currentRotation;
    public float smoothTime = 0.3f;
    public float angleSmoothTime = 0.3f;

    public bool isRotationRig = false;
    public bool isPositionRig = false;

    public Quaternion saveRotation;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("MyPlane").transform;
    }

    void LateUpdate()
    {
        //transform.position = Vector3.Lerp(transform.position, playerPos.position, Time.deltaTime * positionLagTime);
        if(isPositionRig)
        {
            transform.position = Vector3.SmoothDamp(transform.position, playerPos.position, ref currnetVelocity, smoothTime);
        }
        else
        {
            transform.position = playerPos.position;
        }
        
        //transform.rotation = Quaternion.Lerp(transform.rotation, playerPos.rotation, Time.deltaTime * rotationLagTime);
        if(isRotationRig)
        {
            transform.rotation = CameraController.SmoothDamp(transform.rotation, playerPos.rotation, ref currentRotation, angleSmoothTime);
        }
        Debug.DrawLine(transform.position, Camera.main.transform.position, UnityEngine.Color.red);

        if(Input.GetButtonDown("Camera"))
        {
            Debug.Log("¿˙¿Â");
            saveRotation = transform.rotation;
        }

        if (Input.GetButtonUp("Camera"))
        {
            transform.rotation = saveRotation;
        }

        if (Input.GetButton("Camera"))
        {
            transform.Rotate(new Vector3(0, Input.mousePositionDelta.x, 0) * 180.0f * Time.deltaTime);
        }

        float wheelDelta = Input.GetAxisRaw("Mouse ScrollWheel");

        Vector3 moveDirection = playerPos.position - Camera.main.transform.position;
        Camera.main.transform.Translate(moveDirection.normalized * Time.deltaTime * wheelDelta * 200.0f);

    }

    public static Quaternion SmoothDamp(Quaternion rot, Quaternion target, ref Quaternion deriv, float time)
    {
        if (Time.deltaTime < Mathf.Epsilon) return rot;
        // account for double-cover
        var Dot = Quaternion.Dot(rot, target);
        var Multi = Dot > 0f ? 1f : -1f;
        target.x *= Multi;
        target.y *= Multi;
        target.z *= Multi;
        target.w *= Multi;
        // smooth damp (nlerp approx)
        var Result = new Vector4(
            Mathf.SmoothDamp(rot.x, target.x, ref deriv.x, time),
            Mathf.SmoothDamp(rot.y, target.y, ref deriv.y, time),
            Mathf.SmoothDamp(rot.z, target.z, ref deriv.z, time),
            Mathf.SmoothDamp(rot.w, target.w, ref deriv.w, time)
        ).normalized;

        // ensure deriv is tangent
        var derivError = Vector4.Project(new Vector4(deriv.x, deriv.y, deriv.z, deriv.w), Result);
        deriv.x -= derivError.x;
        deriv.y -= derivError.y;
        deriv.z -= derivError.z;
        deriv.w -= derivError.w;

        return new Quaternion(Result.x, Result.y, Result.z, Result.w);
    }
}
