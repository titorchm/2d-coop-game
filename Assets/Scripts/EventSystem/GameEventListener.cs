using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomUnityEvent : UnityEvent<object> { }
public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public CustomUnityEvent Response;

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
