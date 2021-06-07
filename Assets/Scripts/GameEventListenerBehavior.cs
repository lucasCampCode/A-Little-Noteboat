using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListenerBehavior : MonoBehaviour, IListener
{
    [SerializeField] private UnityEvent _actions;
    [SerializeField] private Event _event;
    [SerializeField] private GameObject _intendedSender;

    // Start is called before the first frame update
    void Start()
    {
        _event.AddListener(this);
    }

    public void Invoke(GameObject sender = null)
    {
        //If the sender is not the intended sender or if the intended sender is null
        if (sender != _intendedSender || !_intendedSender)
        {
            _actions.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
