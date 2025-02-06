using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomGameEvent : UnityEvent<object> { }
public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public CustomGameEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }
    
    public void OnEventRaised(object data)
    {
        Response.Invoke(data);
    }
    
}
