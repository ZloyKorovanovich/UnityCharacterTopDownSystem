using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponAimer : MonoBehaviour
{
    [SerializeField]
    private WeaponInHand _currentWeapon;
    [SerializeField]
    private float _absoluteDamage = 10;
    [SerializeField]
    private LayerMask _weaponLayer;
    [SerializeField]
    private Transform _rightHand;
    
    private float _damage;

    public float Damage => _damage;


    private void Awake()
    {
        _damage = _absoluteDamage;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
            TryToPickUp(2);
        if (_currentWeapon)
            _currentWeapon.SetPosition();
    }

    public void RemoveWeapon()
    {
        if(_currentWeapon)
            Instantiate(_currentWeapon.WeaponOnGround, _currentWeapon.transform.position, _currentWeapon.transform.rotation);;
        _currentWeapon = null;
        _damage = _absoluteDamage;
    }

    private void TryToPickUp(float MaxDistance)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        WeaponOnGround weapon;
        if (Physics.Raycast(ray, out hit, 100f, _weaponLayer))
            weapon = hit.collider.GetComponent<WeaponOnGround>();
        else
            return;
        _currentWeapon = Instantiate(weapon.PickWeapon(), _rightHand).GetComponent<WeaponInHand>();
        _currentWeapon.SetPosition();
        _damage = _currentWeapon.Damage;
    }

}
