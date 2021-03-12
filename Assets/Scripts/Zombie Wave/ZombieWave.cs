using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieWave : MonoBehaviour
{
    [SerializeField] private List<Zombie> _zombieWave;
    [SerializeField] private GameObject _graveyard;
    [SerializeField] private WaveMovement _mover;

    private int _spreadModifier = 20;

    public event UnityAction PlayerLost;
    public event UnityAction PlayerWin;
    public event UnityAction<int> ZombieDied;    

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
            var randomIndex = Random.Range(0, _spreadModifier);
            var zombie = _zombieWave[randomIndex];

            zombie.WinCheckpointReached -= OnCheckpoinReached;
            _zombieWave.Remove(zombie);
            ZombieDied?.Invoke(_zombieWave.Count);

            if(_spreadModifier > _zombieWave.Count)
            {
                _spreadModifier = _zombieWave.Count;
            }

            zombie.transform.SetParent(_graveyard.transform);
            zombie.Dying();
        }
        else
        {
            Time.timeScale = 0.3f;
            PlayerWin?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnCheckpoinReached()
    {
        _mover.Stop();
        PlayerLost?.Invoke();
        Time.timeScale = 0.3f;

        foreach (var zombie in _zombieWave)
        {
            zombie.WinCheckpointReached -= OnCheckpoinReached;
            zombie.Stop();
        }
    }

    public int Count()
    {
        return _zombieWave.Count;
    }
}
