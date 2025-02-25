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
        byte[] appearanceData = request.Payload;
        
        response.Approved = true;
        response.CreatePlayerObject = true;
        response.Position = Vector3.zero;
        
        _playerAppearanceService.AssignPlayerAppearance(appearanceData);
    }
    
    private void HandleClientConnected(ulong clientId)
    {
        SetPlayerAppearanceClientRpc(clientId);
    }

    [Rpc(SendTo.Server, AllowTargetOverride = true)]
    private void SetPlayerAppearanceClientRpc(ulong clientId)
    {
        Debug.Log("SetPlayerAppearance called");
        _playerAppearanceService.SetPlayerAppearance(clientId);
    }
}