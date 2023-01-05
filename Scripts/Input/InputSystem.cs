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
    [SerializeField]
    private int _team;
    [SerializeField]
    private Team _teamSystem;

    private Vector3 _target;
    private bool _isAttack;
    private Vector3 _axis;

    private InputInterface _inputer;

    public Team TeamSystem => _teamSystem;
    public int Team => _team;


    private void Awake()
    {
        if (!_charcterMain)
            _charcterMain = GetComponent<CharacterMain>();
        _inputer = GetComponent<InputInterface>();
        if (!_teamSystem)
            _teamSystem = FindObjectOfType<Team>();
    }

    private void OnEnable()
    {
        _teamSystem.Add(this);
    }

    private void OnDisable()
    {
        _teamSystem.Remove(this);
    }

    public void SetTargetPosition(Vector3 Position)
    {
        _target = CountTargetPosition(Position, _lerpPositionParametr);
    }

    private Vector3 CountTargetPosition(Vector3 TargetPosition, float LerpParam)
    {
        TargetPosition = Vector3.Lerp(_target, TargetPosition + _targetOffset, Time.deltaTime * LerpParam);
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
        _charcterMain.SetInput(_axis, _target, _isAttack);
    }


    public void Death()
    {
        _inputer.Delete();
        _charcterMain.Delete();
        Destroy(this);
    }
}
