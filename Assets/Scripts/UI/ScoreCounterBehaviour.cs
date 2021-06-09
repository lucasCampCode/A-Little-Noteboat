using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounterBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameManagerBehaviour _gameManager;

    [SerializeField]
    private Text _displayText;

    // Start is called before the first frame update
    void Start()
    {
        _displayText.text = "" + 0;
    }

    // Update is called once per frame
    void Update()
    {
        _displayText.text = "" + _gameManager.Score;
    }
}
