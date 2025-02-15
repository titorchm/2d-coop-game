using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ClientHost : MonoBehaviour
{
    [SerializeField] private Button startGame;
    
    [Inject]
    private SessionService _sessionService;
    
    private void Awake()
    {
        startGame.onClick.AddListener(HostGame);
    }

    private void HostGame()
    {
        _sessionService.HostGame();
    }
}
