using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MainInput
{
    [SerializeField]
    private Vector3 _camOffset = new Vector3(0, 10, -2.5f);
    [SerializeField]
    private Vector3 _targetOffset = new Vector3(0, 1, 0);

    private CharacterMain _charMain;

    private Camera _camMain;
    private Transform _camTrans;

    private Vector3 _targetPos;
    private Vector3 _inputAxis;
    private bool _isAttack;

    private RaycastHit _hit;

    private void Start()
    {
        _charMain = GetComponent<CharacterMain>();
        _camMain = Camera.main;
        _camTrans = _camMain.transform;
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

        var ray = _camMain.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit, 100f, -1, QueryTriggerInteraction.Ignore))
            _targetPos = Vector3.Lerp(_targetPos, _hit.point + _targetOffset, Time.deltaTime * 5f);

        _charMain.SetInputs(_inputAxis, _targetPos, Input.GetMouseButtonUp(0));

        _camTrans.position = Vector3.Lerp(_camTrans.position, transform.position + _camOffset, Time.deltaTime * 5f);
        var oldRot = _camTrans.eulerAngles;
        _camTrans.LookAt(transform);
        oldRot.x = Mathf.LerpAngle(oldRot.x, _camTrans.eulerAngles.x, Time.deltaTime * 5f);
        oldRot.y = 0;
        oldRot.z = 0;
        _camTrans.eulerAngles = oldRot;
    }
}
