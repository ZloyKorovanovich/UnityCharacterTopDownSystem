using UnityEngine;

public class Attack : MonoBehaviour
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
        if(Physics.Raycast(transform.position, _direction, out hit, _attackLength))
        {
            Collider[] targets = Physics.OverlapSphere(hit.point, _attackWidth, _attackableLayer);
            for(int i = 0; i < targets.Length; i++)
            {
                IDamageble damageble = targets[i].GetComponent<IDamageble>();
                if (damageble != null)
                {
                    float distance = Vector3.Distance(hit.point, targets[i].transform.position);
                    damageble.TakeDamage(_damage, _attackStrength.Evaluate(1 - (distance / _attackLength)));
                }
            }
        }
        Destroy(this);
    }
}
