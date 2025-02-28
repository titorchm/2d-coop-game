using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Services
{
    public class PlayerAppearanceService
    {
        [Inject] private PlayerAppearance _playerAppearance;
        
        [Inject] private AppearanceData _appearanceData;
        
        private NetworkManager m_NetworkManager;
        
        [Inject]
        public PlayerAppearanceService(NetworkManager networkManager)
        {
            m_NetworkManager = networkManager;
        }

        public void SetPlayerAppearance(ulong playerId)
        {
            if (m_NetworkManager.LocalClient.ClientId != playerId)
            {
                return;
            }
            
            //NetworkObject player = m_NetworkManager.ConnectedClients[playerId].PlayerObject;

            NetworkObject player = m_NetworkManager.LocalClient.PlayerObject;
            
            SpriteRenderer[] spriteRenderers = player.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                switch (spriteRenderer.gameObject.name)
                {
                    case "Body":
                        spriteRenderer.sprite = _playerAppearance.body;
                        break;
                    case "Face":
                        spriteRenderer.sprite = _playerAppearance.face;
                        break;
                    case "Eyes":
                        spriteRenderer.sprite = _playerAppearance.eyes;
                        break;
                    case "Hat":
                        spriteRenderer.sprite = _playerAppearance.hat;
                        break;
                }
            }
        }
        
        public void SetPlayerAppearancePayload(int bodyItem, int faceItem, int eyesItem, int hatItem)
        {
            m_NetworkManager.NetworkConfig.ConnectionData = new byte[4];
            m_NetworkManager.NetworkConfig.ConnectionData[0] = (byte)bodyItem;
            m_NetworkManager.NetworkConfig.ConnectionData[1] = (byte)faceItem;
            m_NetworkManager.NetworkConfig.ConnectionData[2] = (byte)eyesItem;
            m_NetworkManager.NetworkConfig.ConnectionData[3] = (byte)hatItem;
        }

        public void AssignPlayerAppearance(byte[] payload)
        {
            _playerAppearance.body = _appearanceData.body[payload[0]];
            _playerAppearance.face = _appearanceData.face[payload[1]];
            _playerAppearance.eyes = _appearanceData.eyes[payload[2]];
            _playerAppearance.hat = _appearanceData.hat[payload[3]];
        }
    }
}
