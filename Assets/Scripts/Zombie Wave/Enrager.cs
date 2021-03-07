using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enrager : MonoBehaviour
{
    [SerializeField] private Zombie _zombie;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.TryGetComponent(out EnrageTrigger trigger))
        {
            _zombie.Enrage();
        }
    }
}
