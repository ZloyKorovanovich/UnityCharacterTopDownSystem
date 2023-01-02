using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    [SerializeField]
    private CharacterMain _charcterMain;
    [SerializeField]
    private Vector3 _targetOffset = new Vector3(0, 1.5f, 0);
    [SerializeField]
    private float _lerpPositionParametr = 5f;
    [SerializeField]
    private float _pickDistance = 2;

    private Transform _target;
    private bool _isAttack;
    private Vector3 _axis;
    

    private void Awake()
    {
        if (!_charcterMain)
            _charcterMain = GetComponent<CharacterMain>();
        _target = Instantiate(new GameObject("Target_" + transform.name), Vector3.zero, Quaternion.Euler(Vector3.zero)).transform;
    }

    public void SetTargetPosition(Vector3 Position)
    {
        _target.position = CountTargetPosition(Position, _lerpPositionParametr);
    }

    private Vector3 CountTargetPosition(Vector3 TargetPosition, float LerpParam)
    {
        TargetPosition = Vector3.Lerp(_target.position, TargetPosition + _targetOffset, Time.deltaTime * LerpParam);
        return TargetPosition;
    }

    public void PickUp(Ray RaycastRay)
    {
        _charcterMain.TryPickup(RaycastRay, _pickDistance);
    }

    public void SetAxis(Vector3 Axis)
    {
        _axis = Axis;
    }

    public void SetAttack(bool Attack)
    {
        _isAttack = Attack;
    }

    public void SetInputs()
    {
        _charcterMain.SetInput(_axis, _target.position, _isAttack);
    }


    public void Death()
    {
        _charcterMain.Delete();
    }
}
