using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A class responsible for the session behaviour.
/// </summary>

public class SessionHandler : NetworkBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private TextMeshProUGUI loadingText;
    
    void Start()
    {
        loadingScreen.SetActive(false);
    }
    
    public void JoinSession(object role)
    {
        NetworkManager.NetworkConfig.ConnectionApproval = true;
        
        loadingScreen.SetActive(true);
        
        if ((ConnectionType)role == ConnectionType.Client)
        {
            NetworkManager.Singleton.StartClient();
        }
        else if ((ConnectionType)role == ConnectionType.Host)
        {
            NetworkManager.Singleton.StartHost();
            
            CustomSceneManager.Instance.LoadNetworkScene(SceneNames.MainWorld, LoadSceneMode.Single);
        }
    }
}
