using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZombiePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _alive;
    [SerializeField] private ZombieWave _wave;

    private void OnEnable()
    {
        _wave.ZombieDied += OnZombieDied;
        _alive.text = _wave.Count().ToString();
    }

    private void OnDisable()
    {
        _wave.ZombieDied -= OnZombieDied;
    }

    private void OnZombieDied(int value)
    {
        _alive.text = value.ToString();
    }
}
