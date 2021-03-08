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

    private readonly string[] Death = { "Death_1", "Death_2", "Death_3", "Death_4", "Death_5" };

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
    }

    public void Dying()
    {
        _target = null;
        _animator.SetBool(Death[Random.Range(0, 5)], true);
    }
}
