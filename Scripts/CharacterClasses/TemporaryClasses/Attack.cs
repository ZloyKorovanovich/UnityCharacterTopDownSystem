using UnityEngine;

public class Attack : CharacterComponent
{
    private float _attackLength;
    private float _attackWidth;
    private float _damage;

    private Vector3 _direction;
    private Vector3 _attackPoint;

    private LayerMask _attackableLayer;

    private AnimationCurve _attackStrength;


    public Attack(Vector3 attackPoint, float attackLength, float attackWidth, float damage, Vector3 direction, LayerMask attackableLayer, AnimationCurve attackStrength)
    {
        _attackPoint = attackPoint;
        _attackLength = attackLength;
        _attackWidth = attackWidth;
        _damage = damage;
        _direction = direction;
        _attackableLayer = attackableLayer;
        _attackStrength = attackStrength;
    }

    public void Attacking()
    {
        RaycastHit hit;
        if(Physics.Raycast(_attackPoint, _direction, out hit, _attackLength, _attackableLayer))
        {
            IDamageble damageble = hit.transform.GetComponent<IDamageble>();
            if (damageble != null)
            {
                float distance = Vector3.Distance(hit.point, _attackPoint);
                damageble.TakeDamage(_damage, _attackStrength.Evaluate(1 - (distance / _attackLength)));
            }
        }
        Destroy(this);
    }
}
