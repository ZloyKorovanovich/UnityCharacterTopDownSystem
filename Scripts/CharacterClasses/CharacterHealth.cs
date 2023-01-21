using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : CharacterComponent
{
    private const string _ANIMATOR_HIT = "Hit";
    private float _health;

    private Animator _animator;


    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
            SetDeath();
        _animator.SetTrigger(_ANIMATOR_HIT);
    }

    private void SetDeath()
    {

    }
}
