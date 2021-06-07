using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounterBehaviour : MonoBehaviour
{
    [Tooltip("The GameManager used to upadate the score being displayed")]
    [SerializeField]
    private GameManagerBehaviour _gameManager;

    [Tooltip("The text that will display the score")]
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
        //Sets the display text to show the current score
        _displayText.text = "" + _gameManager.Score;
    }
}
