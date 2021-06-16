using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void GameEvent();

public class GameManagerBehaviour : MonoBehaviour
{
    public static GameEvent onGameOver;

    private bool _gameStart = false;

    //Used to make sure things are not constantly being set every update
    private bool _changeVar = false;

    [SerializeField]
    private HealthBehaviour _playerHealth;

    [SerializeField]
    private GameObject _startScreen;

    [SerializeField]
    private GameObject _HUD;

    [SerializeField]
    private GameObject _pauseScreen;

    [SerializeField]
    private GameObject _gameOverScreen;

    private static bool _gameOver = false;

    private bool _gamePaused;

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

    public void LoadCredit()
    {
        SceneManager.LoadScene(1);
    }

    public void PauseGame()
    {
        _gamePaused = !_gamePaused;
        if (_gamePaused)
        {
            _HUD.SetActive(false);
            _pauseScreen.SetActive(true);
        }
        else
        {
            _pauseScreen.SetActive(false);
            _HUD.SetActive(true);
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
        //Checks if the game started and if there is a start screen
        if (!_gameStart && _startScreen || _gamePaused)
        {
            //Sets the time scale to 0 to pause the game
            Time.timeScale = 0;
            return;
        }
        //Checks if changeVar is false and if there is a start screen
        else if (!_changeVar && _startScreen)
        {
            //Deactivates start screen, and activates HUD
            _startScreen.SetActive(false);
            _HUD.SetActive(true);

            //Sets time scale back to default
            Time.timeScale = 1;
            _changeVar = true;
        }

        _gameOver = _playerHealth.Health <= 0;

        _gameOverScreen.SetActive(_gameOver);
    }
}
