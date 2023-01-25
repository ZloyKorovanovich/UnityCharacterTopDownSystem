using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInHand : MonoBehaviour
{
    [SerializeField]
    private WeaponOnGround _weaponOnGround;

    private float _attackLength;
    private float _attackWidth;
    private float _attackSpeed;
    private float _damage;

    private AnimationCurve _distanceInfluence;
    private LayerMask _attackableLayer;


    public float AttackLength => _attackLength;
    public float AttackWidth => _attackWidth;
    public float AttackSpeed => _attackSpeed;
    public float Damage => _damage;

    public AnimationCurve DistanceInfluence => _distanceInfluence;
    public LayerMask AttackableLayer => _attackableLayer;

}
