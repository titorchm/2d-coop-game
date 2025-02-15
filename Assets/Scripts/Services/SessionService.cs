using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SessionService
{
    [Inject]
    private SceneService _sceneService;
    
    public void HostGame()
    {
        NetworkManager.Singleton.StartHost();
        _sceneService.LoadNetworkScene(SceneNames.MainWorld, LoadSceneMode.Single);
    }
    
    public void JoinServer(string networkAddress, ushort port = 7777)
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(networkAddress, port);
        NetworkManager.Singleton.StartClient();
    }
}