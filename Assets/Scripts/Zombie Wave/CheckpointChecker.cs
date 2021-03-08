using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointChecker : MonoBehaviour
{
    public event UnityAction WinCheckpointReached;
    public event UnityAction EnragerReached;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.TryGetComponent(out Checkpoint checkpoint))
        {
            WinCheckpointReached?.Invoke();
        }

        if (collision.TryGetComponent(out Enrager enrager))
        {
            EnragerReached?.Invoke();
        }
    }
}
