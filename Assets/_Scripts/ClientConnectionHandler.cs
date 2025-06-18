using Services;
using UnityEngine;
using Unity.Netcode;
using Zenject;

public class ClientConnectionHandler : MonoBehaviour
{
    [Inject]
    private NetworkManager m_NetworkManager;

    [Inject]
    private PlayerAppearanceService _playerAppearanceService;
    
    [SerializeField] private GameEvent onClientConnected;
    
    private PlayerAppearanceData _playerAppearance;

    private void Awake()
    {
        if (m_NetworkManager != null)
        {
            m_NetworkManager.ConnectionApprovalCallback = ApprovalCheck;
            m_NetworkManager.OnClientConnectedCallback += HandleClientConnected;
        }
    }

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        _playerAppearance.bodyIndex = request.Payload[0];
        _playerAppearance.faceIndex = request.Payload[1];
        _playerAppearance.eyesIndex = request.Payload[2];
        _playerAppearance.hatIndex = request.Payload[3];
        
        response.Approved = true;
        response.CreatePlayerObject = true;
        response.Position = Vector3.zero;
    }
    
    private void HandleClientConnected(ulong clientId)
    {
        if (!m_NetworkManager.IsServer) return;
        
        _playerAppearance.playerId = clientId;
        
        _playerAppearanceService.SetPlayerAppearance(_playerAppearance);
        
        // custom event (scriptable object)
        onClientConnected.Raise(clientId);
    }
}