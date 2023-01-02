using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WeaponOnGround : DropItem
{
    [SerializeField]
    private GameObject _WeaponInHand;


    public GameObject PickWeapon()
    {
        Destroy(gameObject);
        return _WeaponInHand;
    }
}
