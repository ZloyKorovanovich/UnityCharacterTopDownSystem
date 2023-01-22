using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInHand : MonoBehaviour
{
    [SerializeField]
    private float _attackLength;
    [SerializeField]
    private float _hitRadius;
    [SerializeField]
    private float _damage;

    [SerializeField]
    private LayerMask _attackableLayer;

    [SerializeField]
    private AnimationCurve _attackStrength;


    public AttackParameters WeaponAttackParameters => new AttackParameters(_attackLength, _hitRadius, _damage, _attackableLayer, _attackStrength);
}
