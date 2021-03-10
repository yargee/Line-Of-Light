using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Zombie : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject[] _deathEffect = new GameObject[6];
    [SerializeField] private GameObject _bloodPool;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CapsuleCollider _collider;

    private readonly string[] _death = { "Death_1", "Death_2", "Death_3", "Death_4" };
    private readonly string _attack = "Attack";
    private readonly string _move = "Move";
    private readonly string _run = "Run";
    private readonly string _idle = "Idle";

    public event UnityAction WinCheckpointReached;    

    private void Awake()
    {
        Physics.IgnoreLayerCollision(10, 10);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Soldier soldier))
        {
            StartCoroutine(Kill());
            soldier.Dying();
        }

        if (collision.TryGetComponent(out Checkpoint checkpoint))
        {
            WinCheckpointReached?.Invoke();
        }
    }

    public void Dying()
    {
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
        Instantiate(_deathEffect[Random.Range(0,1)], new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90,0,0));
        Instantiate(_bloodPool, new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), Quaternion.Euler(90, 0, 0));
        _animator.SetBool(_death[Random.Range(0, 4)], true);
    }

    public IEnumerator Kill()
    {
        _animator.SetBool(_attack, true);
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool(_attack, false);
    }

    public void Move()
    {
        _animator.SetBool(_move, true);
    }

    public void Run()
    {
        _animator.SetBool(_run, true);
    }

    public void Stop()
    {
        _animator.SetBool(_idle, true);
    }    
}
