using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour, ITargetable
{   
    private Health _health;

    public event UnityAction<Enemy> Lost;

    public Transform Transform => transform;

    private void Start()
    {
        _health = GetComponent<Health>();
    }

    public void BeAttacked(int damage)
    {
        _health.TakeDamage(damage);

        if (_health.HealthPoints <= 0)
        {
            Lost?.Invoke(this);
        }
    }
}
