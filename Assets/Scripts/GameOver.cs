using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject _losePanel;
    [SerializeField] GameObject _winPanel;
    [SerializeField] Light[] _lights = new Light[3];
    [SerializeField] ZombieWave _wave;

    private void OnEnable()
    {
        _wave.PlayerLost += OnPlayerLost;
        _wave.PlayerWin += OnPlayerWin;
    }

    private void OnPlayerWin()
    {
        _winPanel.SetActive(true);
        StartCoroutine(LightOff());
    }

    private void OnDisable()
    {
        _wave.PlayerLost -= OnPlayerLost;
    }

    private void OnPlayerLost()
    {
        _losePanel.SetActive(true);
        StartCoroutine(LightOff());
    }

    private IEnumerator LightOff()
    {
        yield return new WaitForSeconds(1.5f);

        foreach (var light in _lights)
        {
            light.enabled = false;            
        }
        Time.timeScale = 0;
        yield break;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
