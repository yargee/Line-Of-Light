using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _fireRate;
    //[SerializeField] private int _accuracy;   добавить потом точность стрельбы, чем ближе к волне, тем больше шанс попадания, так же точность рандомна у каждого солдата
    
    private WaitForSeconds _delay;
    private bool reload = false;
    private Vector3 _bulletSpawnOffset;

    private void Start()
    {
        _delay = new WaitForSeconds(_fireRate);
        _bulletSpawnOffset = new Vector3(1.5f, 1.7f, -0.2f);

    }

    public IEnumerator Shoot(ZombieWave target)
    {
        if (reload == false)
        {
            var bulletFlyOffset = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z + Random.Range(-10, 11));
            Rotate(bulletFlyOffset);
            var bullet = Instantiate(_bullet, transform.position + _bulletSpawnOffset, Quaternion.identity);
            bullet.Init(bulletFlyOffset);            
            reload = true;
            yield return _delay;
            reload = false;
        }
    }

    public void SetFireRate(float rate)
    {
        _fireRate = rate;
    }

    private void Rotate(Vector3 target)
    {
        var direction = (target - transform.position).normalized;

        transform.rotation = Quaternion.LookRotation(direction);
    }
}
