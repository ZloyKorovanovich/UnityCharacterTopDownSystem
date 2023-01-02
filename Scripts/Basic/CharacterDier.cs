using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDier : MonoBehaviour
{
    [SerializeField]
    private float _health = 100;
    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private string _animHit = "Hit";

    private bool _isDaed = false;

    public float Helath => _health;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void TakeDamage(float Damage)
    {
        if (_isDaed)
            return;
        float health = _health - Damage;
        if(health <= 0)
        {
            _health = 0;
            Death();
            return;
        }
        _health = health;
        _anim.SetTrigger(_animHit);
    }

    private void Death()
    {
        _isDaed = true;
        GetComponent<InputSystem>().Death();
    }
}
