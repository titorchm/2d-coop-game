using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A class responsible for the session behaviour.
/// </summary>

public class SessionHandler : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private TextMeshProUGUI loadingText;
    
    void Start()
    {
        loadingScreen.SetActive(false);
    }
    
    public void JoinSession(object role)
    {
        loadingScreen.SetActive(true);
        
        if ((string)role == "Client")
        {
            NetworkManager.Singleton.StartClient();
        }
        else if ((string)role == "Host")
        {
            NetworkManager.Singleton.StartHost();
            NetworkManager.Singleton.SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }
    }
}
