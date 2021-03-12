using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoldierPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _available;
    [SerializeField] private TMP_Text _alive;
    [SerializeField] private SoldierSpawner _spawner;

    private int _aliveSoldiers = 0;

    private void OnEnable()
    {
        _spawner.SoldierPlaced += OnSoldierPlaced;
        _spawner.SoldierDied += OnSoldierDied;
    }

    private void OnDisable()
    {
        _spawner.SoldierPlaced -= OnSoldierPlaced;
        _spawner.SoldierDied -= OnSoldierDied;
    }

    private void OnSoldierDied()
    {
        _aliveSoldiers--;
        _alive.text = _aliveSoldiers.ToString();
    }

    private void OnSoldierPlaced(int number)
    {
        _available.text = number.ToString();
        _aliveSoldiers++;
        _alive.text = _aliveSoldiers.ToString();
    }
}
