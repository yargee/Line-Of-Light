using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    private WaitForSeconds _delay = new WaitForSeconds(1f);
    private bool _attackAvailable = true;

    public bool AttackAvailable => _attackAvailable;

    public IEnumerator Delay()
    {
        _attackAvailable = false;
        yield return _delay;
        _attackAvailable = true;
        yield break;
    }

}
