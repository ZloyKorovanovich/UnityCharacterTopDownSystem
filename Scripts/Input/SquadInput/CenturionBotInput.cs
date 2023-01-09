using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenturionBotInput : MonoBehaviour, SquadBrain
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _team;
    [SerializeField]
    private CenturionBotInput _temporaryEnemySquadField;

    private List<WarriorSystem> _tragets = new List<WarriorSystem>();

    private List<WarriorSystem> _warriors = new List<WarriorSystem>();
    private int _commanderIndex = -1;


    private void Start()
    {
        CheckSquadNumber();
        if (_commanderIndex < 0)
            _commanderIndex = 1;
    }

    private void Update()
    {
        FolowActiveLeader();
    }

    private void FolowActiveLeader()
    {
        transform.position = Vector3.Lerp(transform.position, _warriors[_commanderIndex].transform.position, Time.deltaTime * _speed);
    }

    public void Add(WarriorSystem I, bool IsCommander)
    {
        _warriors.Add(I);
        CheckCommander(IsCommander);
    }

    public void Remove(WarriorSystem I, bool IsCommander)
    {
        _warriors.Remove(I);
        CheckSquadNumber();
        CheckCommander(IsCommander);
    }

    private void CheckCommander(bool IsCommander)
    {
        if (IsCommander)
            _commanderIndex = _warriors.Count - 1;
    }

    private void CheckSquadNumber()
    {
        if (_warriors.Count == 0)
            DestroySquad();
    }

    private void DestroySquad()
    {
        Destroy(this);
    }

    public void GetGetMyWarriors(out List<WarriorSystem> WarriorsOutput)
    {
        WarriorsOutput = _warriors;
    }

    private void GetEnemyWarriors()
    {
        _temporaryEnemySquadField.GetGetMyWarriors(out _tragets);
    }

    public InputSystem GetClosestEnemy(int Team, out float Distance)
    {
        float closestDistanceMagnitude = Mathf.Infinity;
        float currentMagnitude;
        InputSystem output = null;
        for (int i = 0; i < _characters.Count; i++)
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
