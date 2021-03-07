using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Soldier _soldier;
    private WaitForSeconds _delay = new WaitForSeconds(0.5f);
    private bool _reload = false;

    public bool Reload => _reload;

    public IEnumerator Shoot()
    {
        var newBullet = Instantiate(_bullet, transform.position, Quaternion.identity);
        var target = _soldier.Target.transform.position + new Vector3(0, 0, Random.Range(-10, 11));
        var direction = (target - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);

        newBullet.Init(target);
        _reload = true;
        yield return _delay;
        _reload = false;
        yield break;
    }
}
