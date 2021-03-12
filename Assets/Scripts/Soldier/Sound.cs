using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] AudioSource _sound;

    private void Awake()
    {
        _sound.pitch = Random.Range(0.75f, 1.25f);
        _sound.volume = Random.Range(0.1f, 0.3f);
    }
}
