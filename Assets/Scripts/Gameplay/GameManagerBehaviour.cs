using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void GameEvent();

public class GameManagerBehaviour : MonoBehaviour
{
    public static GameEvent onGameOver;

    private bool _gameStart = false;

    private bool _changeVar = false;

    [SerializeField]
    private HealthBehaviour _playerHealth;

    [SerializeField]
    private GameObject _startScreen;

    [SerializeField]
    private GameObject _HUD;

    [SerializeField]
    private GameObject _gameOverScreen;

    private static bool _gameOver = false;

    //Keeps track of the players score
    private float _score;

    public static bool GameOver
    {
        get { return _gameOver; }
    }

    public float Score
    {
        get { return _score; }
        set { _score = value; }
    }

    public void StartGame()
    {
        _gameStart = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameStart)
        {
            Time.timeScale = 0.001f;
            return;
        }
        else if (!_changeVar)
        {
            _startScreen.SetActive(false);
            _HUD.SetActive(true);
            Time.timeScale = 1;
            _changeVar = true;
        }

        _gameOver = _playerHealth.Health <= 0;

        _gameOverScreen.SetActive(_gameOver);
    }
}
