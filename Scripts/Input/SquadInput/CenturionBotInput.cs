using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenturionBotInput : SquadBrain
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _team;
    [SerializeField]
    private CenturionBotInput _enemySquad;
    [SerializeField]
    private float _radiusSquad = 5;

    private List<WarriorSystem> _targets = new List<WarriorSystem>();
    [SerializeField]
    private List<WarriorSystem> _warriors = new List<WarriorSystem>();
    private int _commanderIndex = -1;


    private void Start()
    {
        CheckSquadNumber();
        if (_commanderIndex < 0)
            _commanderIndex = 0;
        if (_enemySquad)
            ChangeTargets();
        SetDestinations();
    }

    private void Update()
    {
        FolowActiveLeader();
    }


    public void GetGetMyWarriors(out List<WarriorSystem> WarriorsOutput)
    {
        WarriorsOutput = _warriors;
    }

    public void RemoveTarget(WarriorSystem Warrior)
    {
        _targets.Remove(Warrior);
        if (_targets.Count == 0)
            return;
        CheckLazyInBattle();
    }

    public override void Add(WarriorSystem I, bool IsCommander)
    {
        _warriors.Add(I);
        CheckCommander(IsCommander);
    }

    public override void Remove(WarriorSystem I, bool IsCommander)
    {
        _warriors.Remove(I);
        CheckSquadNumber();
        if (_enemySquad)
            _enemySquad.RemoveTarget(I);
        CheckCommander(IsCommander);
    }


    private void SetDestinations()
    {
        for(int i = 0; i < _warriors.Count; i++)
        {
            float randomDist = Random.Range(0, _radiusSquad);
            float randomAngle = Random.Range(0, 360);
            Vector3 randomPos = new Vector3(transform.position.x + randomDist * Mathf.Cos(randomAngle), 0, transform.position.y + randomDist * Mathf.Sin(randomAngle));

            _warriors[i].AssignMovePosition(randomPos);
        }
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
            if (j >= _targets.Count)
                j = Random.Range(0, _targets.Count - 1);
            _warriors[i].AssignTarget(_targets[j].GetComponent<InputSystem>());
        }
    }

    private void RemoveTargets()
    {
        for (int i = 0; i < _warriors.Count; i++)
        {
            _warriors[i].AssignTarget(null);
        }
    }

    private void ChangeTargets()
    {
        RemoveTargets();
        AssignTargets();
    }

    private void CheckLazyInBattle()
    {
        bool lazy;
        for(int i = 0; i < _warriors.Count; i++)
        {
            if (!_warriors[i])
                continue;
            lazy = _warriors[i].GetLazy();
            if (lazy)
                _warriors[i].AssignTarget(GetClosestTarget(_warriors[i].transform.position).GetComponent<InputSystem>());
        }
    }

    private void CheckCommander(bool IsCommander)
    {
        if (IsCommander)
            _commanderIndex = 0;
    }

    private WarriorSystem GetClosestTarget(Vector3 Position)
    {
        if (!_enemySquad)
            return null;
        float closestMagnitude = Mathf.Infinity;
        WarriorSystem closest = null;
        float currentMagnitude;
        for(int i = 0; i < _targets.Count; i++)
        {
            currentMagnitude = Vector3.SqrMagnitude(Position - _targets[i].transform.position);
            if(currentMagnitude < closestMagnitude)
            {
                closestMagnitude = currentMagnitude;
                closest = _targets[i];
            }
        }
        return closest;
    }

    private bool CheckSquadNumber()
    {
        if (_warriors.Count == 0)
        {
            DestroySquad();
            return false;
        }
        return true;
    }

    private void DestroySquad()
    {
        Destroy(this.gameObject);
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
