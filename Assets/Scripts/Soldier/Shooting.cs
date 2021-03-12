using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _fireRate;
    [SerializeField] private GameObject _missEffect;
    [SerializeField] private int _accuracy;
    [SerializeField] AudioSource _shotSound;

    private WaitForSeconds _delay;
    private bool reload = false;
    private Vector3 _bulletSpawnOffset;

    public AudioSource ShotSound => _shotSound;

    private void Start()
    {
        _delay = new WaitForSeconds(_fireRate);
        _bulletSpawnOffset = new Vector3(1.5f, 1.7f, -0.2f);
        _shotSound.pitch = Random.Range(0.85f, 1.15f);
        _shotSound.volume = Random.Range(0.2f, 0.3f);
    }

    public IEnumerator Shoot(ZombieWave target)
    {
        if (reload == false)
        {
            var bulletFlyOffset = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z + Random.Range(-10, 11));
            Rotate(bulletFlyOffset);
            var bullet = Instantiate(_bullet, transform.position + _bulletSpawnOffset, Quaternion.identity);
            _shotSound.Play();
            var hit = CheckHit();
            bullet.Init(bulletFlyOffset, hit);

            if(hit == false)
            {
                Instantiate(_missEffect, transform.position + new Vector3(0, 5, 0), Quaternion.identity);
            }

            reload = true;
            yield return _delay;
            reload = false;
        }
    }

    public void SetCharacteristics(float rate, int accuracy)
    {
        _fireRate = rate;
        _accuracy = accuracy;
    }

    private void Rotate(Vector3 target)
    {
        var direction = (target - transform.position).normalized;

        transform.rotation = Quaternion.LookRotation(direction);
    }

    private bool CheckHit()
    {
        return _accuracy >= Random.Range(0, 100);
    }
}
