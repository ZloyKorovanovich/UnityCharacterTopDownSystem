using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInput : WarriorSystem
{
    [SerializeField]
    private InputSystem _input;
    [SerializeField]
    private SquadBrain _myCommander;
    [SerializeField]
    private bool _isCommander;
    [SerializeField]
    private float _stoppingDistance = 1f;

    private InputSystem _target;
    private Vector3 _destination;
    private Vector3 _inputAxis;


    public override void Delete()
    {
        Destroy(this);
    }

    public override void AssignTarget(InputSystem Target)
    {
        _target = Target;
    }

    public override void AssignMovePosition(Vector3 Position)
    {
        _destination = Position;
    }
    public override bool GetLazy()
    {
        if(_target)
            return true;
        return false;
    }


    private void Awake()
    {
        if (!_input)
            _input = GetComponent<InputSystem>();
    }

    private void Update()
    {
        if (_target)
        {
            FolowTarget();
            return;
        }
        _input.SetAttack(false);
        FolowDestination();
    }

    private void OnEnable()
    {
        if (_myCommander != null)
            _myCommander.Add(this, _isCommander);
    }

    private void OnDisable()
    {
        if (_myCommander != null)
            _myCommander.Remove(this, _isCommander);
    }

    private void FolowDestination()
    {
        float distance = Vector3.Distance(transform.position, _destination);
        if(distance > 1)
        {
            _inputAxis.z = distance - _stoppingDistance;
            _input.SetTargetPosition(_destination);
        }
        else
        {
            _input.SetTargetPosition(transform.forward * 3);
            _inputAxis = Vector3.zero;
        }

        _input.SetAxis(_inputAxis);
        _input.SetInputs();
    }

    private void FolowTarget()
    {
        float distance = Vector3.Distance(transform.position, _target.transform.position);
        _inputAxis.z = distance - _stoppingDistance;
        if (distance < 2f)
        {
            if (distance > 1)
                _input.SetAttack(true);
            else
            {
                _input.SetAttack(false);
                _inputAxis.z = -_inputAxis.z;
            }
        }
        else
            _input.SetAttack(false);
        _input.SetAxis(_inputAxis);
        _input.SetTargetPosition(_target.transform.position);
        _input.SetInputs();
    }
}
