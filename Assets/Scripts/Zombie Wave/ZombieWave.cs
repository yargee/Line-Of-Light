using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWave : MonoBehaviour
{
    [SerializeField] private List<Zombie> _zombieWave;
    [SerializeField] private GameObject _graveyard;
    [SerializeField] private WaveMovement _mover;
    [SerializeField] private CheckpointChecker _checker;    

    private void OnEnable()
    {
        _checker.WinCheckpointReached += OnCheckpoinReached;
        _checker.EnragerReached += OnEnragerReached;
    }

    private void FixedUpdate()
    {
        _mover.Move();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Soldier soldier) && _zombieWave.Count > 0)
        {
            var zombie = FindClosestZombie(soldier.transform.position);
            StartCoroutine(zombie.Kill());
            soldier.Dying();
        }
    }

    public void SpreadDamage()
    {
        if (_zombieWave.Count > 0)
        {
            int randomIndex = Random.Range(0, _zombieWave.Count);
            var zombie = _zombieWave[randomIndex];
            _zombieWave.Remove(zombie);
            zombie.transform.SetParent(_graveyard.transform);
            zombie.Dying();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Zombie FindClosestZombie(Vector3 target)
    {
        Zombie closestZombie = _zombieWave[0];

        foreach (var zombie in _zombieWave)
        {
            if (Vector3.Distance(target, zombie.transform.position) < Vector3.Distance(target, closestZombie.transform.position))
            {
                closestZombie = zombie;
            }
        }
        return closestZombie;
    }
    private void OnCheckpoinReached()
    {
        _mover.Stop();
        foreach (var zombie in _zombieWave)
        {
            zombie.Stop();
        }
    }

    private void OnEnragerReached()
    {
        _mover.Boost();
        foreach (var zombie in _zombieWave)
        {
            zombie.Run();
        }
    }
}
