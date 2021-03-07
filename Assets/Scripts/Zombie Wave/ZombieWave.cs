using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWave : MonoBehaviour
{
    [SerializeField] private List<Zombie> _zombieWave;
    [SerializeField] private GameObject _graveyard;

    private void OnEnable()
    {
        foreach (var zombie in _zombieWave)
        {
            zombie.Died += OnZombieDied;
        }
    }

    public void SpreadDamage(int damage)
    {
        if (_zombieWave.Count > 0)
        {
            int randomIndex = Random.Range(_zombieWave.Count / 2, _zombieWave.Count);
            _zombieWave[randomIndex].Health.TakeDamage(damage);
        }
    }

    private void OnZombieDied(Zombie zombie)
    {
        zombie.Died -= OnZombieDied;
        zombie.transform.SetParent(_graveyard.transform);
        _zombieWave.Remove(zombie);

        if (_zombieWave.Count == 0)
        {
            Destroy(gameObject);
            Debug.Log("YOU WIN!!!");
        }
    }
}
