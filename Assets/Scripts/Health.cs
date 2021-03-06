using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _healthPoints;

    public int HealthPoints => _healthPoints;

    public void TakeDamage(int value)
    {
        _healthPoints -= value;
        if (HealthPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
