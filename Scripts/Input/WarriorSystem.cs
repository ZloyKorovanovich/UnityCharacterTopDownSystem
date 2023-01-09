using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WarriorSystem : MonoBehaviour
{
    public abstract void Delete();
    public abstract void AssignTarget(InputSystem Target);
    public abstract void AssignMovePosition(Vector3 Position);
}
