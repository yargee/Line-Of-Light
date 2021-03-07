using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private bool _fade = false;

    public bool Fade => _fade;

    public void StartFade()
    {
        _fade = true;
    }
}
