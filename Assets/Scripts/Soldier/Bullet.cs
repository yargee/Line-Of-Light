using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;    

    private ZombieWave _target;
    private int _shotAccuracy;

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Fly();
    }

    public void Init(ZombieWave target)
    {
        _target = target;        
    }

    private void Fly()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position + new Vector3(0, 0, Random.Range(-10, 11)), _speed);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out ZombieWave wave))
        {
            wave.SpreadDamage();
            Destroy(gameObject);
        }
    }
}
