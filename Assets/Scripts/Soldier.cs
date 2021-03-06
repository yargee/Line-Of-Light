using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private GameObject _spawnEffectTemplate;

    private void OnEnable()
    {
        Instantiate(_spawnEffectTemplate, transform.position, Quaternion.identity);
    }
}
