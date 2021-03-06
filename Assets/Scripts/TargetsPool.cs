using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsPool : MonoBehaviour
{
    [SerializeField] private List<Enemy> _targets;

    private void Awake()
    {
        _targets = new List<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy target))
        {            
            _targets.Add(target);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy target) && _targets.Count > 0)
        {
            _targets.Remove(target);
        }
    }

    public Enemy GetTarget()
    {
        return _targets[Random.Range(0,_targets.Count)];
    }

    public int Count()
    {
        return _targets.Count;
    }
}
