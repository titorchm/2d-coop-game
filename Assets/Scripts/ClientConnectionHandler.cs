using System.Collections.Generic;
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
    
    [SerializeField] private List<uint> alternatePlayerPrefabs;

    private void Awake()
    {
        if (m_NetworkManager != null)
        {
            m_NetworkManager.ConnectionApprovalCallback = ApprovalCheck;
        }
    }

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        byte[] appearanceData = request.Payload;
        
        response.Approved = true;
        response.CreatePlayerObject = true;
        response.Position = Vector3.zero;
        
        PlayerSetAppearanceRpc(appearanceData, request.ClientNetworkId);
    }

    [Rpc(SendTo.Server)]
    private void PlayerSetAppearanceRpc(byte[] appearanceData, ulong playerId)
    {
        _playerAppearanceService.SetPlayerAppearance(appearanceData, m_NetworkManager.ConnectedClients[playerId].PlayerObject);
    }
}