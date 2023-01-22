using UnityEngine;

public class Attacker
{
    private AttackParameters _attackParameters;


    public Attacker(AttackParameters attackParameters)
    {
        _attackParameters = attackParameters;
    }

    public void Attack(Vector3 position, Vector3 direction)
    {
        Attacking(_attackParameters, position, direction);
    }

    private void Attacking(AttackParameters attackParameters, Vector3 position, Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.SphereCast(position, attackParameters.HitRadius, direction, out hit, attackParameters.AttackLength, attackParameters.AttackableLayer))
        {
            IDamageble target = hit.transform.GetComponent<IDamageble>();
            if (target != null)
            {
                float time = Vector3.Distance(position, hit.point) / attackParameters.AttackLength;
                target.TakeDamage(attackParameters.Damage, attackParameters.AttackStrength.Evaluate(time));
            }
        }
    }
}
