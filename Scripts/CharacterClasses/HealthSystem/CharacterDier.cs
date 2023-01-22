using UnityEngine;

public class CharacterDier
{
    private static string _ANIMATOR_DEATH = "death";

    private Animator _animator;


    public static void SetPublicDeath()
    {

    }

    public static void SetActiveRagDoll(Transform root, bool active)
    {
        Rigidbody[] parts = root.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < parts.Length; i++)
            parts[i].isKinematic = active;
    }
}
