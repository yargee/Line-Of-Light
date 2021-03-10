using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Soldier : MonoBehaviour
{
    [SerializeField] private GameObject _spawnEffectTemplate;
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private Shooting _shooter;
    [SerializeField] private ZombieWave _target;
    [SerializeField] private GameObject _bloodPool;
    [SerializeField] private GameObject[] _deathEffect = new GameObject[6];    

    private readonly string[] Death = { "Death_1", "Death_2", "Death_3", "Death_4", "Death_5" };
    private bool _alive = true;

    public event UnityAction Died;

    private void OnEnable()
    {
        Instantiate(_spawnEffectTemplate, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (_target == null)
        {
            return;
        }

        StartCoroutine(_shooter.Shoot(_target));
    }

    public void Init(ZombieWave target)
    {
        _target = target;
        _shooter.SetFireRate(Random.Range(0.8f, 1.5f));
    }

    public void Dying()
    {
        if (_alive)
        {
            _collider.enabled = false;
            Died?.Invoke();
            _alive = false;
            Instantiate(_deathEffect[Random.Range(0, 6)], new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90, 0, 0));
            Instantiate(_bloodPool, new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), Quaternion.Euler(90, 0, 0));
            _target = null;
            _animator.SetBool(Death[Random.Range(0, 5)], true);
        }
    }
}
