using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int _hardness;
    [SerializeField] private GameObject _breakEffect;

    public void TryBreak()
    {
        _hardness--;
        if(_hardness <=0)
        {
            Instantiate(_breakEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
