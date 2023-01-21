using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class CharacterMain : Character
{
    [SerializeField]
    private LayerMask _attackableLayer;

    private Animator _animator;
    private CharacterController _characterController;

    private Vector3 _inputAxis;
    private Vector3 _target;


    private bool _isRotating;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        Death.DisableRagDoll(transform);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        _characterController.Move(Physics.gravity * Time.deltaTime);
        CharacterMover.SetPublicMove(_animator, _inputAxis, Time.deltaTime, transform, _target, ref _isRotating);
    }


    public void SetMovementInputs(Vector3 inputAxis, Vector3 target)
    {
        _inputAxis = inputAxis;
        _target = target;
    }


}
