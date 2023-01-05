using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    private List<InputSystem> _characters = new List<InputSystem>(); 

    public void Add(InputSystem Additive)
    {
        _characters.Add(Additive);
    }

    public void Remove(InputSystem Subtractive)
    {
        _characters.Remove(Subtractive);
    }

    public InputSystem GetClosestEnemy(Vector3 Position, int Team, out float Distance)
    {
        float closestDistanceMagnitude = Mathf.Infinity;
        float currentMagnitude;
        InputSystem output = null;
        for(int i = 0; i < _characters.Count; i++)
        {
            if (_characters[i].Team == Team)
                continue;
            currentMagnitude = Vector3.SqrMagnitude(Position - _characters[i].transform.position);
            if (currentMagnitude <= closestDistanceMagnitude)
            {
                output = _characters[i];
                closestDistanceMagnitude = currentMagnitude;
            }
        }
        Distance = Mathf.Sqrt(closestDistanceMagnitude);
        return output;
    }
}
