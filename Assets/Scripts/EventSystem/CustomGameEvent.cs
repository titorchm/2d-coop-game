using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomGameEvent", menuName = "ScriptableObjects/CustomGameEvent")]
public class CustomGameEvent : ScriptableObject
{
    private List<CustomGameEventListener> _listeners = new();

    public void Raise(object data1, object data2)
    {
        for (int i = 0; i < _listeners.Count; i++)
        {
            _listeners[i].OnEventRaised(data1, data2);
        }
    }

    public void RegisterListener(CustomGameEventListener listener)
    {
        _listeners.Add(listener);
    }

    public void UnregisterListener(CustomGameEventListener listener)
    {
        _listeners.Remove(listener);
    }
}