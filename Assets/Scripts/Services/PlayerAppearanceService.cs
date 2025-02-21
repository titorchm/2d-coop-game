using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Services
{
    public class PlayerAppearanceService
    {
        [Inject]
        private AppearanceData _appearanceData;
        
        private NetworkManager m_NetworkManager;
        
        [Inject]
        public PlayerAppearanceService(NetworkManager networkManager)
        {
            m_NetworkManager = networkManager;
        }

        public void SetPlayerAppearance(byte[] playerAppearance, NetworkObject player)
        {
            SpriteRenderer[] spriteRenderers = player.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                switch (spriteRenderer.gameObject.name)
                {
                    case "Body":
                        spriteRenderer.sprite = _appearanceData.body[playerAppearance[0]];
                        break;
                    case "Face":
                        spriteRenderer.sprite = _appearanceData.body[playerAppearance[1]];
                        break;
                    case "Eyes":
                        spriteRenderer.sprite = _appearanceData.body[playerAppearance[2]];
                        break;
                    case "Hat":
                        spriteRenderer.sprite = _appearanceData.body[playerAppearance[3]];
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
    }
}
