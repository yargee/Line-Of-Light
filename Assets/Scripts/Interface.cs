using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ITargetable
{
    Transform Transform { get; }

    void BeAttacked(int value);

    event UnityAction<Enemy> Lost;
}
