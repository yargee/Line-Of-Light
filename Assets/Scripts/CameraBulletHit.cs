﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBulletHit : MonoBehaviour
{
    [SerializeField] private GameObject _brokenGlassEffect;

    private Quaternion _previousPosition;
    private WaitForSeconds _delay = new WaitForSeconds(0.1f);
    private int _chanceToHit = 1;
    private int _hitRoll;

    private void FixedUpdate()
    {
        _hitRoll = Random.Range(0, 200);
        if(_hitRoll <_chanceToHit)
        {
            BulletHit();
        }
    }

    public IEnumerator Shake()
    {
        _previousPosition = transform.rotation;
        transform.Rotate((Random.Range(-0.2f, 0.2f)), (Random.Range(-0.2f, 0.2f)), (Random.Range(-0.2f, 0.2f)));
        yield return _delay;
        transform.rotation = _previousPosition;
        yield break;
    }

    public void BulletHit()
    {
        var hit = Instantiate(_brokenGlassEffect, transform.position, Quaternion.Euler(new Vector3(-35, 90, 0)));
        hit.transform.SetParent(transform);
        StartCoroutine(Shake());
    }

}
