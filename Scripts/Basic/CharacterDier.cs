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

    public float Helath => _health;

    public void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void TakeDamage(float Damage)
    {
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
        DestroyComponents();
    }

    private void DestroyComponents()
    {
        CharacterPart[] parts = GetComponentsInChildren<CharacterPart>();
        for (int i = 0; i < parts.Length; i++)
            Destroy(parts[i]);
        ICharacterComponent component = GetComponent<ICharacterComponent>();
        component.Delete();
    }
}
