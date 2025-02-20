using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Zenject;

public class ClientConnectionHandler : MonoBehaviour
{
    [Inject]
    private NetworkManager m_NetworkManager;
    
    [SerializeField] private List<uint> alternatePlayerPrefabs;

    private void Start()
    {
        if (m_NetworkManager != null)
        {
            m_NetworkManager.ConnectionApprovalCallback = ApprovalCheck;
        }
    }

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        var playerPrefabIndex = System.BitConverter.ToInt32(request.Payload);
        
        if (alternatePlayerPrefabs.Count > playerPrefabIndex)
        {
            response.Approved = true;
            response.PlayerPrefabHash = alternatePlayerPrefabs[playerPrefabIndex];
            response.CreatePlayerObject = true;
            response.Position = Vector3.zero;
        }
        else
        {
            response.Approved = false;
            response.Reason = "";
        }
    }
}