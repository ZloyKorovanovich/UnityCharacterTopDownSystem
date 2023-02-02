using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MainInput
{
    [SerializeField]
    private Vector3 _cameraOffset = new Vector3(0, 10, -2.5f);
    [SerializeField]
    private Vector3 _targetOffset = new Vector3(0, 1, 0);

    private MainComponent _charMain;

    private Camera _cameraMain;
    private Transform _cameraTransform;

    private Vector3 _targetPosition;
    private Vector3 _inputAxis;
    private bool _isAttack;

    private RaycastHit _hit;

    private void Start()
    {
        _charMain = GetComponent<CharacterMain>();
        _cameraMain = Camera.main;
        _cameraTransform = _cameraMain.transform;
    }

    private void LateUpdate()
    {
        var angle = transform.eulerAngles.y * Mathf.Deg2Rad;
        var cosAngle = Mathf.Cos(angle);
        var sinAngle = Mathf.Sin(angle);
        var vert = cosAngle * Input.GetAxis("Vertical") + sinAngle * Input.GetAxis("Horizontal");
        var hor = cosAngle * Input.GetAxis("Horizontal") - sinAngle * Input.GetAxis("Vertical");

        _inputAxis = new Vector3(hor, 1, vert);

        _isAttack = Input.GetMouseButton(0);

        var ray = _cameraMain.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit, 100f, -1, QueryTriggerInteraction.Ignore))
            _targetPosition = Vector3.Lerp(_targetPosition, _hit.point + _targetOffset, Time.deltaTime * 5f);

        _charMain.SetInputs(_inputAxis, _targetPosition, Input.GetMouseButtonUp(0));

        CountCameraMovement();
    }

    private void CountCameraMovement()
    {
        _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, transform.position + _cameraOffset, Time.deltaTime * 5f);
        var oldRot = _cameraTransform.eulerAngles;
        _cameraTransform.LookAt(transform);
        oldRot.x = Mathf.LerpAngle(oldRot.x, _cameraTransform.eulerAngles.x, Time.deltaTime * 5f);
        oldRot.y = 0;
        oldRot.z = 0;
        _cameraTransform.eulerAngles = oldRot;
    }
}
