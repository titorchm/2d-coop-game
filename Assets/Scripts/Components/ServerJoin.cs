using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Services;

public class ServerJoin : MonoBehaviour
{
    [SerializeField] private TMP_InputField networkAddressField;
    [SerializeField] private Button joinButton;

    [Inject]
    private SessionStartService _sessionService;
    
    private void Awake()
    {
        joinButton.onClick.AddListener(OnJoin);
    }

    private void OnJoin()
    {
        _sessionService.JoinServer(networkAddressField.text);
    }
}
