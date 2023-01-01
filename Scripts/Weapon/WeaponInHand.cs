using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInHand : MonoBehaviour
{
    [SerializeField]
    private GameObject _weaponOnGround;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private Vector3 _position;
    [SerializeField]
    private Vector3 _rotation;

    public float Damage => _damage;
    public GameObject WeaponOnGround => _weaponOnGround;


    public void SetPosition()
    {
        transform.localPosition = _position;
        transform.localRotation = Quaternion.Euler(_rotation);
    }
}
