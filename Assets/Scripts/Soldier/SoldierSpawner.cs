using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoldierSpawner : MonoBehaviour
{
    [SerializeField] private Soldier _template;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private ZombieWave _target;
    [SerializeField] private GameObject _soldierDiedEffect;
    [SerializeField] private GameObject _spawnPointEffect;
    [SerializeField] private int _availableSoldiers;

    private WaitForSeconds _spawnDelay = new WaitForSeconds(1);
    private List<Soldier> _soldiers = new List<Soldier>();
    private RaycastHit _hit;
    private Ray _ray;
    private bool soundEnabled = true;

    public event UnityAction<int> SoldierPlaced;
    public event UnityAction SoldierDied;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit) && SpawnPositionFree(_hit.point)  && _availableSoldiers > 0)
            {
                Instantiate(_spawnPointEffect, _hit.point + new Vector3(0, 0.1f, 0), Quaternion.Euler(-90, 0, 0));
                StartCoroutine(SpawnSoldier(_hit.point));
                SoldierPlaced?.Invoke(_availableSoldiers);
            }
        }
    }

    private bool SpawnPositionFree(Vector3 point)
    {
        if (_soldiers.Count == 0)
        {
            return true;
        }

        foreach (var soldier in _soldiers)
        {
            if (Vector3.Distance(soldier.transform.position, point) >= _spawnDistance)
            {
                continue;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator SpawnSoldier(Vector3 spawnPoint)
    {
        var newSoldier = Instantiate(_template, spawnPoint, Quaternion.Euler(new Vector3(0, 90, 0)));
        _availableSoldiers--;
        newSoldier.Died += OnDied;
        newSoldier.Init(_target, soundEnabled);
        soundEnabled = !soundEnabled;
        _soldiers.Add(newSoldier);
        yield return _spawnDelay;
        newSoldier.gameObject.SetActive(true);
        yield break;
    }

    private void OnDied()
    {
        SoldierDied?.Invoke();
        Instantiate(_soldierDiedEffect, transform.position, Quaternion.Euler(new Vector3(-35, 90, 0)));
    }
}
