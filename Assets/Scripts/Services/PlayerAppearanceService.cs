using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Services
{
    public class PlayerAppearanceService
    {
        private NetworkManager m_NetworkManager;
        
        [Inject]
        public PlayerAppearanceService(NetworkManager networkManager)
        {
            m_NetworkManager = networkManager;
        }

        public void SetPlayerAppearance(int bodyItem, int faceItem, int eyesItem, int hatItem)
        {
            m_NetworkManager.NetworkConfig.ConnectionData = new byte[4];
            
            m_NetworkManager.NetworkConfig.ConnectionData[0] = (byte)bodyItem;
            m_NetworkManager.NetworkConfig.ConnectionData[1] = (byte)faceItem;
            m_NetworkManager.NetworkConfig.ConnectionData[2] = (byte)eyesItem;
            m_NetworkManager.NetworkConfig.ConnectionData[3] = (byte)hatItem;
        }
    }
}
