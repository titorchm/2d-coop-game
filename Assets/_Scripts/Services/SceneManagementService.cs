using Unity.Netcode;
using UnityEngine.SceneManagement;

namespace Services
{
    public class SceneManagementService
    {
        public void LoadNetworkScene(SceneNames sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            NetworkManager.Singleton.SceneManager.LoadScene(sceneName.ToString(), mode);
        }
    }
    
    public enum SceneNames
    {
        MainMenu,
        MainWorld
    }
}