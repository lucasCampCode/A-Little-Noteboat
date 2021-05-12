using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "EventSystem/Event")]
public class Event : ScriptableObject
{
    private List<IListener> _listeners = new List<IListener>();

    /// <summary>
    /// Adds a listener to the list of listeners
    /// </summary>
    /// <param name="newListener">The new Listener</param>
    public void AddListener(IListener newListener)
    {
        _listeners.Add(newListener);
    }

    /// <summary>
    /// Calls Invoke for all listeners in the list of listeners
    /// </summary>
    /// <param name="sender">The one who called Raise</param>
    public void Raise(GameObject sender = null)
    {
        foreach (IListener listener in _listeners)
        {
            listener.Invoke(sender);
        }
    }
}