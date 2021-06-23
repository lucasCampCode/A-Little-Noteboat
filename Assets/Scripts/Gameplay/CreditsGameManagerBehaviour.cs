using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsGameManagerBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseScreen;

    private bool _gamePaused;

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        _gamePaused = !_gamePaused;
        if (_gamePaused)
            _pauseScreen.SetActive(true);

        else
        {
            _pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
