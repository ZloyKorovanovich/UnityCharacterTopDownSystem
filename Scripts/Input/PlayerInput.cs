using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour, InputInterface
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
    [SerializeField]
    private SquadBrain _myCommander;

    private Transform _cameraTransform;
    private InputSystem _input;


    public void Delete()
    {
        Destroy(this);
    }

    private void Awake()
    {
        if (!_characterCamera)
            _characterCamera = Camera.main;
        _cameraTransform = _characterCamera.transform;

        _input = GetComponent<InputSystem>();
    }

    private void LateUpdate()
    {
        float angle = transform.eulerAngles.y * Mathf.Deg2Rad;
        _input.SetAxis(GetAxis(angle));
        _input.SetAttack(Input.GetMouseButtonUp(0));
        _input.SetTargetPosition(GetRaycastPosition(_groundLayer));
        _input.SetInputs();
        if (Input.GetMouseButtonUp(1))
            _input.PickUp(Camera.main.ScreenPointToRay(Input.mousePosition));
        CountCameraMovement(_cameraTransform, transform, _CameraOffset, out _cameraTransform, _lerpTargetPositionParam);
    }

    private void OnEnable()
    {
        if (_myCommander != null)
            _myCommander.Add(this, true);
    }

    private void OnDisable()
    {
        if (_myCommander != null)
            _myCommander.Remove(this, true);
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

    private Vector3 GetRaycastPosition(LayerMask GroundLayer)
    {
        Vector3 output = Vector3.zero;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f, GroundLayer, QueryTriggerInteraction.Ignore))
            output = hit.point;
        return output;   
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
