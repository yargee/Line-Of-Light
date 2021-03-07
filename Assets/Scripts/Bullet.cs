using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private Vector3 _target;

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Fly();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out ZombieWave wave))
        {
            wave.SpreadDamage(_damage);
            Destroy(gameObject);
        }
    }

    public void Init(Vector3 target)
    {
        _target = target;
    }

    public void Fly()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed);
    }
}
