using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{
    [SerializeField] private Soldier _template;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private ZombieWave _target;

    private WaitForSeconds _spawnDelay = new WaitForSeconds(1);
    private List<Soldier> _soldiers = new List<Soldier>();
    private RaycastHit _hit;
    private Ray _ray;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit) && SpawnPositionFree(_hit.point))
            {
                StartCoroutine(SpawnSoldier(_hit.point));
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
        newSoldier.Init(_target);
        _soldiers.Add(newSoldier);
        yield return _spawnDelay;
        newSoldier.gameObject.SetActive(true);
        yield break;
    }
}
