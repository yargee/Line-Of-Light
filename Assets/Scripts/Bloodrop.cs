using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodrop : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.TryGetComponent(out Camera camera))
        {
            Debug.Log("Blood");
        }
    }
}
