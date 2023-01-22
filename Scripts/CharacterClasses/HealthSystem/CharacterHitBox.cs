using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CharacterHitBox : HitBox, IDamageble
{
    [SerializeField]
    private float _resistion;

    private CharacterMain _characterMain;

    private void Awake()
    {
        _characterMain = GetComponentInParent<CharacterMain>();
    }

    public void TakeDamage(float damage, float distanceInfluence)
    {
        _characterMain.TakeDamage(damage * _resistion, distanceInfluence * _resistion);
    }
}
