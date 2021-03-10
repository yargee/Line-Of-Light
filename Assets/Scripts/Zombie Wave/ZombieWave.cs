using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWave : MonoBehaviour
{
    [SerializeField] private List<Zombie> _zombieWave;
    [SerializeField] private GameObject _graveyard;
    [SerializeField] private WaveMovement _mover;    

    private void OnEnable()
    {
        foreach (var zombie in _zombieWave)
        {
            zombie.WinCheckpointReached += OnCheckpoinReached;            
        }
    }

    private void FixedUpdate()
    {
        _mover.Move();
    }

    public void SpreadDamage()
    {
        if (_zombieWave.Count > 0)
        {
            var randomIndex = Random.Range(0, _zombieWave.Count / 8);
            var zombie = _zombieWave[randomIndex];

            zombie.WinCheckpointReached -= OnCheckpoinReached;            
            _zombieWave.Remove(zombie);
            zombie.transform.SetParent(_graveyard.transform);
            zombie.Dying();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCheckpoinReached()
    {
        _mover.Stop();

        foreach (var zombie in _zombieWave)
        {
            zombie.WinCheckpointReached -= OnCheckpoinReached;
            zombie.Stop();
        }
    }
}
