using UnityEngine;

public class CharacterHealth
{
    private float Health;

    public void DamageHealth(float damage, float distanceInfluence, Animator animator)
    {
        CharacterDamager.TakePublicDamage(animator, damage, distanceInfluence, out damage);
        Health -= damage;
        if (Health <= 0)
            CharacterDier.SetPublicDeath();
    }
}
