using UnityEngine;

public class Death : MonoBehaviour
{
    private static string _ANIMATOR_DEATH = "Death";

    public static void DisableRagDoll(Transform myRoot)
    {
        Rigidbody[] parts = myRoot.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < parts.Length; i++)
            parts[i].isKinematic = true;
    }

    public static void Die(Transform myRoot, bool enableRD, float animatorDisableTime, Animator _animator)
    {
        Character characterMainComponent = myRoot.GetComponent<Character>();
        Destroy(characterMainComponent);

        CharacterComponent[] characterComponents = myRoot.GetComponentsInChildren<CharacterComponent>();
        for (int i = 0; i < characterComponents.Length; i++)
            Destroy(characterComponents[i]);

        if (!enableRD)
            _animator.SetBool(_ANIMATOR_DEATH, true);
        else
            ActivateRagDoll(myRoot.GetComponentsInChildren<Rigidbody>());
        
        Destroy(_animator, animatorDisableTime);
    }

    private static void ActivateRagDoll(Rigidbody[] parts)
    {
        for (int i = 0; i < parts.Length; i++)
            parts[i].isKinematic = false;
    }
}
