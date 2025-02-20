using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Services;

public class ClientHost : MonoBehaviour
{
    [SerializeField] private Button startGame;
    
    [Inject]
    private SessionStartService _sessionService;
    
    private void Awake()
    {
        startGame.onClick.AddListener(HostGame);
    }

    private void HostGame()
    {
        _sessionService.HostGame();
    }
}
