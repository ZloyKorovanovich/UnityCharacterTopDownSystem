using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SquadBrain : MonoBehaviour
{
    public abstract void Add(WarriorSystem I, bool IsCommander);
    public abstract void Remove(WarriorSystem I, bool IsCommander);
}
