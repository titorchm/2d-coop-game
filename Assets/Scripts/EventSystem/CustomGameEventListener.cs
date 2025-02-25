using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomUnityEventExtention : UnityEvent<object, object> { }
public class CustomGameEventListener : MonoBehaviour
{
    public CustomGameEvent Event;
    public CustomUnityEventExtention Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }
    
    public void OnEventRaised(object data1, object data2)
    {
        Response.Invoke(data1, data2);
    }
    
}