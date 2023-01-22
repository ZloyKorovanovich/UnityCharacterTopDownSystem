using UnityEngine;

public class CharacterDamager
{
    private static string _ANIMATOR_HIT = "Hit";

    private Animator _animator;

    public CharacterDamager(Animator animator)
    {
        _animator = animator;
    }

    public void TakeDamage(float damage, float distanceInfluence, out float damageFinal)
    {
        damageFinal = damage * distanceInfluence;
        _animator.SetTrigger(_ANIMATOR_HIT);
    }

    public static void TakePublicDamage(Animator animator, float damage, float distanceInfluence, out float damageFinal)
    {
        damageFinal = damage * distanceInfluence;
        animator.SetTrigger(_ANIMATOR_HIT);
    }
}
