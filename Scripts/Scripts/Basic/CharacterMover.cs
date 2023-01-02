using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private string _animVert = "Vert";
    [SerializeField]
    private string _animHor = "Hor";
    [SerializeField]
    private string _animState = "State";
    [SerializeField]
    private string _animRot = "Rot";
    [SerializeField]
    private string _animIsRot = "IsRot";
    [SerializeField]
    private float _sensetivity = 7f;
    [SerializeField]
    private float _luft = 60f;

    private bool _isRotating;


    private void Awake()
    {
        if (!_anim)
            _anim = GetComponent<Animator>();
    }

    public void SetAnimatorStats(Vector3 Axis, float DeltaTime)
    {
        _anim.SetFloat(_animVert, Axis.z, 1f / _sensetivity, DeltaTime);
        _anim.SetFloat(_animHor, Axis.x, 1f / _sensetivity, DeltaTime);
        _anim.SetFloat(_animState, 1f, 1f / _sensetivity, DeltaTime);
    }

    private void SetLook(Vector3 Position)
    {
        _anim.SetLookAtWeight(1f, 0.7f, 0.9f, 1f, 1f);
        _anim.SetLookAtPosition(Position);
    }

    public void Move(Transform Body, Vector3 Target, Vector3 Axis, float DeltaTime)
    {
        SetLook(Target);
        bool predicate = false;
        if (Mathf.Abs(Axis.x) < Mathf.Epsilon && Mathf.Abs(Axis.z) < Mathf.Epsilon)
            predicate = true;
        Rotate(Body, Target, predicate, DeltaTime);
    }

    private void Rotate(Transform Body, Vector3 Target, bool Rotating, float DeltaTime)
    {
        Vector3 oldRotation = Body.eulerAngles;
        Body.LookAt(Target);

        float angle = Mathf.DeltaAngle(Body.eulerAngles.y, oldRotation.y);
        _anim.SetFloat(_animRot, -Mathf.Sign(angle), 1f / _sensetivity, DeltaTime);

        angle = Mathf.Abs(angle);
        DeltaTime *= _sensetivity;

        if (!Rotating)
            oldRotation.y = Mathf.LerpAngle(oldRotation.y, Body.eulerAngles.y, DeltaTime);
        else if (angle > _luft)
            _isRotating = true;

        Body.eulerAngles = oldRotation;

        if (!_isRotating)
            return;
        if (angle * Mathf.Deg2Rad <= DeltaTime)
        {
            _isRotating = false;
            Rotating = false;
        }

        _anim.SetBool(_animIsRot, Rotating);
    }

    private Vector3 CalculateLocalInputs(string VertAxis, string HorAxis)
    {

        float x = Input.GetAxis(VertAxis);
        float z = Input.GetAxis(HorAxis);

        return new Vector3(z, 0, x).normalized;
    }
}
