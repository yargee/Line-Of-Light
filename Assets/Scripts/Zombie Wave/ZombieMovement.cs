using UnityEngine;
using UnityEngine.Events;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _enragedSpeed = 10;
    private Vector3 _direction = new Vector3(0, 0, 1);

    public void Move()
    {
        transform.Translate(_direction * _speed);
    }

    public void Stop()
    {
        _speed = 0;
    }

    public void Boost()
    {
        _speed *= _enragedSpeed;
    }
}
