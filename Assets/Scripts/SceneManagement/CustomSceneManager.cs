using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for loading scenes with custom behaviour.
/// </summary>

public class CustomSceneManager : NetworkBehaviour
{
    public static CustomSceneManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneAsync(SceneNames sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        SceneManager.LoadSceneAsync(sceneName.ToString(), mode);
    }
    
    public void LoadNetworkScene(SceneNames sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(sceneName.ToString(), mode);
    }
}

public enum SceneNames
{
    MainMenu,
    CharacterSelection,
    MainWorld,
}