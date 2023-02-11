using UnityEngine;

public class TopDownController
{
    private Vector3 _cameraOffset;
    private Vector3 _targetOffset;
    private Transform _bodyTransform;


    public TopDownController(Vector3 cameraOffset, Vector3 targetOffset, Transform bodyTransform)
    {
        _cameraOffset = cameraOffset;
        _targetOffset = targetOffset;
        _bodyTransform = bodyTransform;
    }

    public void CountCameraMovement(ref Transform camera)
    {
        camera.position = Vector3.Lerp(camera.position, _bodyTransform.position + _cameraOffset, Time.deltaTime * 5f);

        Vector3 oldRot = camera.eulerAngles;
        camera.LookAt(_bodyTransform);

        oldRot.x = Mathf.LerpAngle(oldRot.x, camera.eulerAngles.x, Time.deltaTime * 5f);
        oldRot.y = 0;
        oldRot.z = 0;

        camera.eulerAngles = oldRot;
    }

    public void CountInputAxis(float verticalInput, float horizontalInput, out Vector3 inputAxis)
    {
        float angle = _bodyTransform.eulerAngles.y * Mathf.Deg2Rad;

        float cosAngle = Mathf.Cos(angle);
        float sinAngle = Mathf.Sin(angle);

        float vertical = cosAngle * verticalInput + sinAngle * horizontalInput;
        float horizontal = cosAngle * horizontalInput - sinAngle * verticalInput;

        inputAxis = new Vector3(horizontal, 1, vertical);
    }
}


