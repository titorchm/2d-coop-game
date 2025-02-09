using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Button actions for session start.
/// </summary>

public class SessionStartActions : MonoBehaviour
{
    [SerializeField] private Button startHost;
    [SerializeField] private Button startClient;
    
    [SerializeField] private GameEvent onPlayerJoin;

    private void Start()
    {
        startHost.onClick.AddListener(StartHost);
        startClient.onClick.AddListener(StartClient);
    }

    private void OnDestroy()
    {
        startHost.onClick.RemoveListener(StartHost);
        startClient.onClick.RemoveListener(StartClient);
    }

    private void StartHost()
    {
        Debug.Log("ClickedHost");
        onPlayerJoin.Raise(ConnectionType.Host);
    }

    private void StartClient()
    {
        onPlayerJoin.Raise(ConnectionType.Client);
    }
}

public enum ConnectionType
{
    Host,
    Client,
    Server
}