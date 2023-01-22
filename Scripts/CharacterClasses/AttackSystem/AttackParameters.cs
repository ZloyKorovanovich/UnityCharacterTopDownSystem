using UnityEngine;

public class AttackParameters
{
    private float _attackLength;
    private float _hitRadius;
    private float _damage;

    private LayerMask _attackableLayer;

    private AnimationCurve _attackStrength;


    public float AttackLength => _attackLength;
    public float HitRadius => _hitRadius;
    public float Damage => _damage;

    public LayerMask AttackableLayer => _attackableLayer;

    public AnimationCurve AttackStrength => _attackStrength;


    public AttackParameters(float attackLength, float hitRadius, float damage, LayerMask attackableLayer, AnimationCurve attackStrength)
    {
        _attackLength = attackLength;
        _hitRadius = hitRadius;
        _damage = damage;
        _attackableLayer = attackableLayer;
        _attackStrength = attackStrength;
    }
}
