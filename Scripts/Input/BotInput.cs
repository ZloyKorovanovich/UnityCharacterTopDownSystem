using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInput : MonoBehaviour, InputInterface
{
    [SerializeField]
    private InputSystem _target;
    [SerializeField]
    private InputSystem _input;

    private Vector3 _inputAxis;


    public void Delete()
    {
        Destroy(this);
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
            FolowTargets();
            return;
        }
        _target = GetTarget();
        _input.SetAttack(false);
        _input.SetAxis(Vector3.zero);
        _input.SetInputs();
    }

    private InputSystem GetTarget()
    {
        float distance;
        var output = _input.TeamSystem.GetClosestEnemy(transform.position, _input.Team, out distance);
        if (output)
            return output;
        return null;
    }

    private void FolowTargets()
    {
        float distance = Vector3.Distance(transform.position, _target.transform.position);
        _inputAxis.z = distance - 1f;
        if(distance < 2.5f)
        {
            if(distance > 1)
            {
                _input.SetAttack(true);
            }
            else
            {
                _inputAxis.z = -_inputAxis.z;
            }
        }
        _input.SetAxis(_inputAxis);
        _input.SetTargetPosition(_target.transform.position);
        _input.SetInputs();
    }
}
