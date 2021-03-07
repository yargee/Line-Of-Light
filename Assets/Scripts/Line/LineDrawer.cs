using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private Line _template;

    private Vector3 _offset = new Vector3(0, -1, 0);
    private Line _currentLine;    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentLine = Instantiate(_template, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        }

        if (_currentLine != null && _currentLine.Fade == false)
        {
            _currentLine.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + _offset;
        }

        if (Input.GetMouseButtonUp(0) && _currentLine != null)
        {
            _currentLine.StartFade();
        }
    }
}
