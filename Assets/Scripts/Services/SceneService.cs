using Unity.Netcode;
using UnityEngine.SceneManagement;

public class SceneService
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