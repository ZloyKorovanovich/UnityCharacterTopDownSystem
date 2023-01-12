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
    private CenturionBotInput _enemySquad;

    private List<WarriorSystem> _targets = new List<WarriorSystem>();

    private List<WarriorSystem> _warriors = new List<WarriorSystem>();
    private int _commanderIndex = -1;


    private void Start()
    {
        CheckSquadNumber();
        if (_commanderIndex < 0)
            _commanderIndex = 0;
    }

    private void Update()
    {
        FolowActiveLeader();
    }


    public void GetGetMyWarriors(out List<WarriorSystem> WarriorsOutput)
    {
        WarriorsOutput = _warriors;
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


    private void FolowActiveLeader()
    {
        transform.position = Vector3.Lerp(transform.position, _warriors[_commanderIndex].transform.position, Time.deltaTime * _speed);
    }

    private void AssignTargets()
    {
        GetEnemyWarriors();
        SortWarriotsList();
        SortTargetsList();
        for(int i = 0; i < _warriors.Count; i++)
        {
            int j = i;
            if (j == _targets.Count)
                j = _targets.Count - 1;
            _warriors[i].AssignTarget(_targets[j].GetComponent<InputSystem>());
        }
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

    private void GetEnemyWarriors()
    {
        _enemySquad.GetGetMyWarriors(out _targets);
    }

    private void SortWarriotsList()
    {
        _warriors = SortObjectsByDistance(_warriors, transform.position);
    }

    private void SortTargetsList()
    {
        _targets = SortObjectsByDistance(_targets, transform.position);
    }

    private List<WarriorSystem> SortObjectsByDistance(List<WarriorSystem> Warriors, Vector3 Position)
    {
        for (int write = 0; write < Warriors.Count; write++)
        {
            for (int sort = 0; sort < Warriors.Count - 1; sort++)
            {
                if (Vector3.SqrMagnitude(Position - Warriors[sort].transform.position) > Vector3.SqrMagnitude(Position - Warriors[sort + 1].transform.position))
                {
                    var temp = Warriors[sort + 1];
                    Warriors[sort + 1] = Warriors[sort];
                    Warriors[sort] = temp;
                }
            }
        }

        return Warriors;
    }
}
