using UnityEngine;

public class CharacterMover : CharacterComponent
{
    //Constant animator parametrs
    private static string _ANIMATOR_VERTICAL = "Vertical";
    private static string _ANIMATOR_HORIZONTAL = "Horizontal";
    private static string _ANIMATOR_STATE = "State";
    private static string _ANIMATOR_ROTATION = "Rotation";
    private static string _ANIMATOR_IS_ROTATING = "IsRotating";

    private Animator _animator;

    private float _sensetivity;
    private float _luft;

    private bool _isRotating;


    private CharacterMover(Animator animator)
    {
        _animator = animator;
        _sensetivity = 7f;
        _luft = 60f;
    }

    public CharacterMover(Animator animator, float sensetivity, float luft)
    {
        _animator = animator;
        _sensetivity = sensetivity;
        _luft = luft;
    }


    public void SetMove(Vector3 axis, float deltaTime, Transform body, Vector3 target)
    {
        SetAnimatorAxisStats(_animator, axis, deltaTime, _sensetivity);
        SetMovement(_animator, body, target, axis, deltaTime, _sensetivity, _luft, ref _isRotating);
    }


    public static void SetPublicMove(Animator animator, Vector3 axis, float deltaTime, Transform body, Vector3 target, ref bool isRotating)
    {
        SetAnimatorAxisStats(animator, axis, deltaTime, 7f);
        SetMovement(animator, body, target, axis, deltaTime, 7f, 60f, ref isRotating);
    }

    public static void SetPublicMove(Animator animator, float sensetivity, float luft, Vector3 axis, float deltaTime, Transform body, Vector3 target, ref bool isRotating)
    {
        SetAnimatorAxisStats(animator, axis, deltaTime, sensetivity);
        SetMovement(animator, body, target, axis, deltaTime, sensetivity, luft, ref isRotating);
    }

    private static void SetAnimatorAxisStats(Animator animator, Vector3 axis, float deltaTime, float sensetivity)
    {
        animator.SetFloat(_ANIMATOR_VERTICAL, axis.z, 1f / sensetivity, deltaTime);
        animator.SetFloat(_ANIMATOR_HORIZONTAL, axis.x, 1f / sensetivity, deltaTime);
        animator.SetFloat(_ANIMATOR_STATE, 1f, 1f / sensetivity, deltaTime);
    }

    private static void SetMovement(Animator animator, Transform body, Vector3 target, Vector3 axis, float deltaTime, float sensetivity, float luft, ref bool isRotating)
    {
        SetLook(animator, target);
        bool tempRotating = false;
        if (Mathf.Abs(axis.x) < Mathf.Epsilon && Mathf.Abs(axis.z) < Mathf.Epsilon)
            tempRotating = true;
        Rotate(animator, body, target, tempRotating, deltaTime, sensetivity, luft, ref isRotating);
    }

    private static void SetLook(Animator animator, Vector3 target)
    {
        animator.SetLookAtWeight(1f, 0.7f, 0.9f, 1f, 1f);
        animator.SetLookAtPosition(target);
    }

    private static void Rotate(Animator animator, Transform body, Vector3 target, bool rotating, float deltaTime, float sensetivity, float luft, ref bool isRotating)
    {
        Vector3 oldRotation = body.eulerAngles;
        body.LookAt(target);

        float angle = Mathf.DeltaAngle(body.eulerAngles.y, oldRotation.y);
        animator.SetFloat(_ANIMATOR_ROTATION, -Mathf.Sign(angle), 1f / sensetivity, deltaTime);

        angle = Mathf.Abs(angle);
        deltaTime *= sensetivity;

        if (!rotating)
            oldRotation.y = Mathf.LerpAngle(oldRotation.y, body.eulerAngles.y, deltaTime);
        else if (angle > luft)
            isRotating = true;

        body.eulerAngles = oldRotation;

        if (!isRotating)
            return;
        else if (angle * Mathf.Deg2Rad <= deltaTime)
        {
            isRotating = false;
            rotating = false;
        }

        animator.SetBool(_ANIMATOR_IS_ROTATING, rotating);
    }
}
