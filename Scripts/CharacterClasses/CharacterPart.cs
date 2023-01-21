using UnityEngine;

public class CharacterPart : CharacterComponent, IDamageble
{
    private float _partHealth;
    private float _partResistion = 1f;
    private CharacterHealth _characterHealth;

    public void TakeDamage(float damage, float distanceInfluence)
    {
        float damageBase = damage * distanceInfluence;
        float characterDamage = damageBase / _partResistion;
        _characterHealth.TakeDamage(characterDamage);

        float myDamage = damageBase * _partResistion;
        _partHealth -= myDamage;
        if (_partHealth <= 0)
            PartDeath();
    }

    private void PartDeath()
    {

    }
}
