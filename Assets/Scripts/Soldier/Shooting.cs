using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _fireRate;
    
    private WaitForSeconds _delay;
    private bool reload = false;

    private void Start()
    {
       _delay = new WaitForSeconds(_fireRate);
    }

    public IEnumerator Shoot(ZombieWave target)
    {
        if(reload == false)
        {
            var bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            bullet.Init(target);
            reload = true;
            yield return _delay;
            reload = false;
        }
    }
}
