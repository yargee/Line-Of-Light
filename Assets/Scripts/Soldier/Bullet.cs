using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _target;
    private bool _hit;

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Fly();
    }

    public void Init(Vector3 target, bool hit)
    {
        _target = target;
        _hit = hit;
    }

    private void Fly()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out ZombieWave wave))
        {
            if (_hit)
            {
                wave.SpreadDamage();
            }            
            Destroy(gameObject);
        }
    }
}
