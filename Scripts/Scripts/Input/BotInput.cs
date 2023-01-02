using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInput : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private InputSystem _input;

    private Vector3 _inputAxis;

    private void Awake()
    {
        if (!_input)
            _input = GetComponent<InputSystem>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.position);
        _inputAxis.z = distance - 1.5f;
        _input.SetAxis(_inputAxis);
        _input.SetAttack(distance < 2.5f);
        _input.SetTargetPosition(_player.position);
        _input.SetInputs();
    }
}
