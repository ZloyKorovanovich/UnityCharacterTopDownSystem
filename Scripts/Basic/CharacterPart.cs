using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPart : MonoBehaviour, IDamageble
{
    [SerializeField]
    private float _strength;
    [SerializeField]
    private CharacterDier _characterDier;

    private void Awake()
    {
        if (!_characterDier)
            _characterDier = GetComponentInParent<CharacterDier>();
    }

    public void TakeDamage(float Damage)
    {
        _characterDier.TakeDamage(Damage * _strength);
    }
}
