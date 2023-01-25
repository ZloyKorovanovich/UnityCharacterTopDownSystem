using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class CharacterMain : MonoBehaviour
{
    private Animator _animator;
    private CharacterController _characterController;

    private Vector3 _inputAxis;
    private Vector3 _target;
    private bool _isAttack;

    private Mover _mover;
    private Attacker _attacker;

    private WeaponInHand _weapon;
    private IDamageble _damageble;

    private float _health;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _mover = new Mover(_animator, 7f, 60f);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        _characterController.Move(Physics.gravity * Time.deltaTime);
        _mover.Move(_inputAxis, Time.deltaTime, transform, _target);
        if(_weapon)
            _attacker.SetAttack(_isAttack);
    }


    public void SetWeapon(WeaponInHand weapon)
    {
        _weapon = weapon;
        ApplyWeapon();
    }

    public void ApplyWeapon()
    {
        _attacker = new Attacker(_animator, _weapon.AttackLength, _weapon.AttackWidth, _weapon.AttackableLayer, _weapon.AttackSpeed);
    }

    public void SetInputs(Vector3 inputAxis, Vector3 target, bool isAttack)
    {
        _inputAxis = inputAxis;
        _target = target;
        _isAttack = isAttack;
    }

    public void Attacking()
    {
        if (_attacker != null)
        {
            _attacker.Attack(_weapon.Damage, _weapon.DistanceInfluence, _damageble);
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }
}
