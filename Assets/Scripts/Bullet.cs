using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private Enemy _target;

    void FixedUpdate()
    {
        if (_target == null)
        {
            return;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.Transform.position, _speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ITargetable target))
        {
            _target.Lost -= OnTargetLost;
            target.BeAttacked(_damage);
            target = null;
            Destroy(gameObject);
        }
    }

    public void Init(Enemy target)
    {
        _target = target;
        _target.Lost += OnTargetLost;
    }

    public void OnTargetLost(Enemy enemy)
    {
        Destroy(gameObject);
    }
}
