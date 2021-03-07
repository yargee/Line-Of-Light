using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private GameObject _spawnEffectTemplate;
    [SerializeField] private ZombieWave _target;
    [SerializeField] private Animator _animator;
    [SerializeField] private Attack _attacker;
    [SerializeField] private BoxCollider _collider;

    private readonly string[] Death = { "Death_1", "Death_2", "Death_3", "Death_4", "Death_5" };
    private bool _alive = true;
    private WaitForSeconds _deathDelay = new WaitForSeconds(0.2f);
    public ZombieWave Target => _target;


    private void OnEnable()
    {
        Instantiate(_spawnEffectTemplate, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (_alive != true || _attacker.Reload == true)
        {
            return;
        }

        if (_target != null)
        {
            StartCoroutine(_attacker.Shoot());
        }
    }

    public void Init(ZombieWave target)
    {
        _target = target;
    }

    public IEnumerator Dying()
    {
        yield return _deathDelay;
        _alive = false;
        _animator.SetBool(Death[Random.Range(0, 5)], true);
        _collider.enabled = false;
        yield break;
    }
}
