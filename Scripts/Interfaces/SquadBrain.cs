using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SquadBrain
{
    void Add(WarriorSystem I, bool IsCommander);
    void Remove(WarriorSystem I, bool IsCommander);
}
