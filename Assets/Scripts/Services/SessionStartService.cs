using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine.SceneManagement;
using Zenject;

namespace Services
{
    public class SessionStartService
    {
        private readonly SceneManagementService _sceneService;
        private readonly NetworkManager m_NetworkManager;
    
        [Inject]
        public SessionStartService(SceneManagementService sceneService, NetworkManager networkManager)
        {
            _sceneService = sceneService;
            m_NetworkManager = networkManager;
        }
    
        public void HostGame()
        {
            m_NetworkManager.StartHost();
            _sceneService.LoadNetworkScene(SceneNames.MainWorld, LoadSceneMode.Single);
        }
    
        public void JoinServer(string networkAddress, ushort port = 7777)
        {
            UnityTransport unityTransport = m_NetworkManager.GetComponent<UnityTransport>();
        
            unityTransport.SetConnectionData(networkAddress, port);
        
            m_NetworkManager.StartClient();
        }
    }
}