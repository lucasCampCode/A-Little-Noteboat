using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void GameEvent();

public class GameManagerBehaviour : MonoBehaviour
{
    public static GameEvent onGameOver;

    [SerializeField]
    private HealthBehaviour _playerHealth;

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
        _gameOver = _playerHealth.Health <= 0;

        _gameOverScreen.SetActive(_gameOver);
    }
}
