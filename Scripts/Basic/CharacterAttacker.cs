using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttacker : MonoBehaviour
{
    [SerializeField]
    private CharacterWeaponAimer _aimer;
    [SerializeField]
    private LayerMask _attackable;
    [SerializeField]
    private float _attackLength;
    [SerializeField]
    private float _attackWidth;
    [SerializeField]
    private float _yOffset;

    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private string _animIsAttack = "IsAttack";
    [SerializeField]
    private float _attackSpeed;

    private Vector3 _direct;


    private void Awake()
    {
        if (!_aimer)
            _aimer = GetComponent<CharacterWeaponAimer>();
    }

    public void Attack(Vector3 Direction)
    {
        _anim.SetTrigger(_animIsAttack);
        _direct = Direction;
    }

    public void Attacking()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position + Vector3.up * _yOffset, _attackWidth, _direct, out hit, _attackLength, _attackable))
        {
            IDamageble enemy = hit.collider.GetComponent<IDamageble>();
            if (enemy == null)
                return;
            enemy.TakeDamage(_aimer.Damage);
        }
    }
}
