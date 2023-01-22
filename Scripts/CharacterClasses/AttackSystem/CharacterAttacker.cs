using System.Collections.Generic;
using UnityEngine;

public class CharacterAttacker
{
    private static string _ANIMATOR_ATTACK = "IsAttack";

    private Animator _animator;

    private AttackParameters _attack;


    public CharacterAttacker(Animator animator, AttackParameters attack)
    {
        _animator = animator;
        _attack = attack;
    }

    public void ChangeAttack(AttackParameters attack)
    {
        _attack = attack;
    }

    public void SetAttack(out Attacker attacker)
    {
        _animator.SetTrigger(_ANIMATOR_ATTACK);
        attacker = new Attacker(_attack);
    }

    public static void SetPublicAttack(AttackParameters attack, out Attacker attacker, Animator animator)
    {
        animator.SetTrigger(_ANIMATOR_ATTACK);
        attacker = new Attacker(attack);
    }
}
