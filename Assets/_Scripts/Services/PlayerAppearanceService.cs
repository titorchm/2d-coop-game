using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Services
{
    public class PlayerAppearanceService
    {
        [Inject] private AppearanceData _appearanceData;
        
        private NetworkManager m_NetworkManager;
        
        [Inject]
        public PlayerAppearanceService(NetworkManager networkManager)
        {
            m_NetworkManager = networkManager;
        }
        
        public void SetPlayerAppearancePayload(int bodyItem, int faceItem, int eyesItem, int hatItem)
        {
            m_NetworkManager.NetworkConfig.ConnectionData = new byte[4];
            m_NetworkManager.NetworkConfig.ConnectionData[0] = (byte)bodyItem;
            m_NetworkManager.NetworkConfig.ConnectionData[1] = (byte)faceItem;
            m_NetworkManager.NetworkConfig.ConnectionData[2] = (byte)eyesItem;
            m_NetworkManager.NetworkConfig.ConnectionData[3] = (byte)hatItem;
        }
        
        public void SetPlayerAppearance(PlayerAppearanceData playerAppearance)
        {
            UpdatePlayerAppearances(playerAppearance);

            ulong playerId = playerAppearance.playerId;
            
            NetworkList<PlayerAppearanceData> playerAppearances = PlayerAppearanceManager.Instance.PlayerAppearances;
            
            NetworkObject player = m_NetworkManager.ConnectedClients[playerId].PlayerObject;
            
            SpriteRenderer[] spriteRenderers = player.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                switch (spriteRenderer.gameObject.name)
                {
                    case "Body":
                        spriteRenderer.sprite = _appearanceData.body[playerAppearances[(int)playerId].bodyIndex];
                        break;
                    case "Face":
                        spriteRenderer.sprite = _appearanceData.face[playerAppearances[(int)playerId].faceIndex];
                        break;
                    case "Eyes":
                        spriteRenderer.sprite = _appearanceData.eyes[playerAppearances[(int)playerId].eyesIndex];
                        break;
                    case "Hat":
                        spriteRenderer.sprite = _appearanceData.hat[playerAppearances[(int)playerId].hatIndex];
                        break;
                }
            }
        }

         private void UpdatePlayerAppearances(PlayerAppearanceData playerAppearance)
         {
             PlayerAppearanceManager.Instance.AddPlayerAppearanceRpc(playerAppearance);
         }
        
        public void SyncPlayerAppearances()
        {
            IReadOnlyDictionary<ulong, NetworkClient> playerClients = m_NetworkManager.ConnectedClients;
            
            NetworkList<PlayerAppearanceData> playerAppearances = PlayerAppearanceManager.Instance.PlayerAppearances;
            
            foreach (var playerClient in playerClients)
            {
                NetworkObject playerObject = playerClient.Value.PlayerObject;

                SpriteRenderer[] spriteRenderers = playerObject.GetComponentsInChildren<SpriteRenderer>();

                foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                {
                    switch (spriteRenderer.gameObject.name)
                    {
                        case "Body":
                            spriteRenderer.sprite =
                                _appearanceData.body[playerAppearances[(int)playerClient.Key].bodyIndex];
                            break;
                        case "Face":
                            spriteRenderer.sprite =
                                _appearanceData.face[playerAppearances[(int)playerClient.Key].faceIndex];
                            break;
                        case "Eyes":
                            spriteRenderer.sprite =
                                _appearanceData.eyes[playerAppearances[(int)playerClient.Key].eyesIndex];
                            break;
                        case "Hat":
                            spriteRenderer.sprite =
                                _appearanceData.hat[playerAppearances[(int)playerClient.Key].hatIndex];
                            break;
                    }
                }
            }
        }
    }
}

public struct PlayerAppearanceData : INetworkSerializable, IEquatable<PlayerAppearanceData>
{
    public ulong playerId;
    public int bodyIndex;
    public int faceIndex;
    public int eyesIndex;
    public int hatIndex;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerId);
        serializer.SerializeValue(ref bodyIndex);
        serializer.SerializeValue(ref faceIndex);
        serializer.SerializeValue(ref eyesIndex);
        serializer.SerializeValue(ref hatIndex);
    }

    public bool Equals(PlayerAppearanceData other)
    {
        return playerId == other.playerId &&
               bodyIndex == other.bodyIndex &&
               faceIndex == other.faceIndex &&
               eyesIndex == other.eyesIndex &&
               hatIndex == other.hatIndex;
    }
}
