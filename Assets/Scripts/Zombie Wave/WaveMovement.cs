using UnityEngine;
using UnityEngine.Events;

public class WaveMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _enragedSpeed = 7;
    private Vector3 _direction = new Vector3(-1, 0, 0);   

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
