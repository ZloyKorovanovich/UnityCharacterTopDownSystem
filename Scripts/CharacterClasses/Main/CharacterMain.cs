using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class CharacterMain : Character
{
    [SerializeField]
    private WeaponInHand _weapon;

    private Animator _animator;
    private CharacterController _characterController;

    private Vector3 _inputAxis;
    private Vector3 _target;

    private Attacker _attacker;
    private CharacterHealth _health;

    private bool _isRotating;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
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

    public void SetAttack()
    {
        CharacterAttacker.SetPublicAttack(_weapon.WeaponAttackParameters, out _attacker, _animator);
    }

    public void Attacking()
    {
        if (_attacker != null)
        {
            _attacker.Attack(transform.position + Vector3.up * 1.4f, _target);
        }
    }

    public void TakeDamage(float damage, float distanceInfluence)
    {
        _health.DamageHealth(damage, distanceInfluence, _animator);
    }
}
