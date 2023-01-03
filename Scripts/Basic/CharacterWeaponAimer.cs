using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponAimer : MonoBehaviour
{
    [SerializeField]
    private GameObject _startWeapon;
    [SerializeField]
    private float _absoluteDamage = 10;
    [SerializeField]
    private LayerMask _weaponLayer;
    [SerializeField]
    private Transform _rightHand;

    private WeaponInHand _currentWeapon;
    private float _damage;

    public float Damage => _damage;


    private void Awake()
    {
        _damage = _absoluteDamage;
        if (_startWeapon)
            SetWeapon(_startWeapon);
    }

    private void SetWeapon(GameObject Weapon)
    {
        if (!Weapon)
            RemoveWeapon();
        _currentWeapon = Instantiate(Weapon, _rightHand).GetComponent<WeaponInHand>();
        if (_currentWeapon)
            _currentWeapon.SetPosition();
        _damage = _currentWeapon.Damage;
    }

    public void RemoveWeapon()
    {
        if (_currentWeapon)
        {
            Instantiate(_currentWeapon.WeaponOnGround, _currentWeapon.transform.position, _currentWeapon.transform.rotation);
            Destroy(_currentWeapon.gameObject);
        }
        _currentWeapon = null;
        _damage = _absoluteDamage;
    }

    public  void TryToPickUp(Ray PickRay, float MaxDistance)
    {
        RaycastHit hit;
        WeaponOnGround weapon;
        if (Physics.Raycast(PickRay, out hit, 100f, _weaponLayer))
            weapon = hit.collider.GetComponent<WeaponOnGround>();
        else
            return;
        RemoveWeapon();
        SetWeapon(weapon.PickWeapon());
    }

}
