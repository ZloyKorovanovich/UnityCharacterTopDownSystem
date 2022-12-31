using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Camera _characterCamera;
    [SerializeField]
    private string _vertAxis = "Vertical";
    [SerializeField]
    private string _horAxis = "Horizontal";
    [SerializeField]
    private Vector3 _targetOffset = new Vector3(0, 1.5f, 0);
    [SerializeField]
    private Vector3 _CameraOffset = new Vector3(0, 10, 2.5f);
    [SerializeField]
    private float _lerpTargetPositionParam = 5f;
    [SerializeField]
    private LayerMask _groundLayer;

    private Transform _cameraTransform;
    private CharacterMain _characterSystem;

    private Vector3 _targetPosition;
    private Vector3 _axisInput;

    private void Awake()
    {
        if (!_characterCamera)
            _characterCamera = Camera.main;
        _cameraTransform = _characterCamera.transform;

        _characterSystem = GetComponent<CharacterMain>();
    }

    private void LateUpdate()
    {
        float angle = transform.eulerAngles.y * Mathf.Deg2Rad;
        _axisInput = GetAxis(angle);

        _targetPosition = CountTargetPosition(_targetPosition, _targetOffset, _lerpTargetPositionParam, _groundLayer);
        _characterSystem.SetInput(_axisInput, _targetPosition, Input.GetMouseButtonUp(0));

        CountCameraMovement(_cameraTransform, transform, _CameraOffset, out _cameraTransform, _lerpTargetPositionParam);
    }

    private void CountCameraMovement(Transform CameraTransform, Transform BodyTransform, Vector3 CameraOffset, out Transform CountedCameraTransform, float LerpParam)
    {
        CountedCameraTransform = CameraTransform;
        Vector3 position = Vector3.Lerp(CameraTransform.position, BodyTransform.position + CameraOffset, Time.deltaTime * LerpParam);
        Vector3 oldRotation = CameraTransform.eulerAngles;
        CameraTransform.LookAt(BodyTransform);
        oldRotation.x = Mathf.LerpAngle(oldRotation.x, CameraTransform.eulerAngles.x, Time.deltaTime * LerpParam);
        oldRotation.y = oldRotation.z = 0;
        Vector3 eulerAngles = oldRotation;
        CountedCameraTransform.position = position;
        CountedCameraTransform.eulerAngles = eulerAngles;
    }

    private Vector3 CountTargetPosition(Vector3 TargetPosition, Vector3 TargetOffset, float LerpParam, LayerMask GroundLayer)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f, GroundLayer, QueryTriggerInteraction.Ignore))
            TargetPosition = Vector3.Lerp(TargetPosition, hit.point + TargetOffset, Time.deltaTime * LerpParam);
        return TargetPosition;
    }

    private Vector3 GetAxis(float Angle)
    {
        float sine = Mathf.Sin(Angle);
        float cosine = Mathf.Cos(Angle);
        float vertAxis = cosine * Input.GetAxis(_vertAxis) + sine * Input.GetAxis(_horAxis);
        float horAxis = cosine * Input.GetAxis(_horAxis) - sine * Input.GetAxis(_vertAxis);
        return new Vector3(horAxis, 1, vertAxis);
    }
}
