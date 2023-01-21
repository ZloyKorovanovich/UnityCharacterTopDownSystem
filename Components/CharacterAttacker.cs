using UnityEngine;

public class CharacterAttacker : CharacterComponent
{
    private static string _ANIMATOR_IS_ATTACK = "IsAttack";

    private Animator _animator;

    private float _attackLength;
    private float _attackWidth;
    private float _damage;

    private AnimationCurve _attackStrength;

    private LayerMask _attackableLayer;

    private GameObject _owner;

    private Transform _attackPoint;


    public CharacterAttacker(Animator animator, float attackLength, float attackWidth, float damage, AnimationCurve attackStrength, LayerMask attackableLayer, GameObject owner, Transform attackPoint)
    {
        _animator = animator;
        _attackLength = attackLength;
        _attackWidth = attackWidth;
        _damage = damage;
        _attackStrength = attackStrength;
        _attackableLayer = attackableLayer;
        _owner = owner;
        _attackPoint = attackPoint;
    }

    public void SetAttack(Vector3 targetPoint, ref Attack currentAttack)
    {
        Attack attack = new Attack(_attackPoint.position, _attackLength, _attackWidth, _damage, targetPoint, _attackableLayer, _attackStrength);
        MakeAttack(_owner, attack, _animator, ref currentAttack);
    }

    public static void SetPublicAttack(Animator animator, float attackLength, float attackWidth, float damage, AnimationCurve attackStrength, LayerMask attackableLayer, GameObject owner, Transform attackPoint, Vector3 targetPoint, ref Attack currentAttack)
    {
        Attack attack = new Attack(attackPoint.position, attackLength, attackWidth, damage, targetPoint, attackableLayer, attackStrength);
        MakeAttack(owner, attack, animator, ref currentAttack);
    }
    
    private static void MakeAttack(GameObject owner, Attack attack, Animator animator, ref Attack currentAttack)
    {
        Attack newAttack = owner.AddComponent<Attack>();
        newAttack = attack;
        animator.SetTrigger(_ANIMATOR_IS_ATTACK);
        currentAttack = newAttack;
    }
}
