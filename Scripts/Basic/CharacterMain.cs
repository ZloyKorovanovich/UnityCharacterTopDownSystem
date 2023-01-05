using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMain : MonoBehaviour
{ 
    private Vector3 _targetPosition;
    private Vector3 _axisInput;

    private bool _isAttack;

    private CharacterController _characterController;

    private CharacterMover _characterMover;
    private CharacterAttacker _characterAttacker;
    private CharacterDier _characterDier;
    private CharacterWeaponAimer _characterAimer;


    public void Delete()
    {
        var aimer = GetComponent<CharacterWeaponAimer>();
        if (aimer)
        {
            aimer.RemoveWeapon();
            Destroy(aimer);
        }
        Destroy(GetComponent<Animator>());
        Destroy(_characterAttacker);
        Destroy(_characterMover);
        Destroy(_characterDier);
        Destroy(_characterController);
        Destroy(this);
        CharacterPart[] charParts = GetComponentsInChildren<CharacterPart>();
        for (int i = 0; i < charParts.Length; i++)
            Destroy(charParts[i].gameObject);
        EnableRagdoll();
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _characterMover = GetComponent<CharacterMover>();
        _characterAttacker = GetComponent<CharacterAttacker>();
        _characterDier = GetComponent<CharacterDier>();
        _characterAimer = GetComponent<CharacterWeaponAimer>();

        DisableRagdoll();
    }

    private void OnAnimatorIK()
    {
        _characterController.Move(Physics.gravity * Time.deltaTime);
        SetAnimator(_axisInput, Time.deltaTime, transform, _targetPosition);

        if (_isAttack)
            _characterAttacker.Attack(_targetPosition - transform.position);
    }

    public void SetInput(Vector3 Axis, Vector3 Target, bool IsAttack)
    {
        _axisInput = Axis;
        _targetPosition = Target;
        _isAttack = IsAttack;
    }

    public void TryPickup(Ray RaycastRay, float MaxDistance)
    {
        _characterAimer.TryToPickUp(RaycastRay, MaxDistance);
    }

    private void DisableRagdoll()
    {
        Rigidbody[] rb = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rb.Length; i++)
            rb[i].isKinematic = true;
    }

    private void EnableRagdoll()
    {
        Rigidbody[] rb = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rb.Length; i++)
        {
            rb[i].isKinematic = false;
        }
    }

    private void SetAnimator(Vector3 Axis, float DeltaTime, Transform Body, Vector3 Target)
    {
        _characterMover.SetAnimatorStats(Axis, DeltaTime);
        _characterMover.Move(Body, Target, Axis, DeltaTime);
    }
}
