using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTextBeahviour : MonoBehaviour
{
    [SerializeField]
    private Text _displayText;
    [SerializeField]
    private HealthBehaviour _gameObject;

    // Start is called before the first frame update
    void Start()
    {
        _displayText.text = "" + 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Sets the display text to show the current score
        _displayText.text = "Health: " + _gameObject.Health;
    }
}
