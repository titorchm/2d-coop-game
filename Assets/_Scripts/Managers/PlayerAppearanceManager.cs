using System.Collections;
using Services;
using Unity.Netcode;
using Zenject;

public class PlayerAppearanceManager : NetworkBehaviour
{
    public static PlayerAppearanceManager Instance { get; private set; }

    public NetworkList<PlayerAppearanceData> PlayerAppearances = new();
    
    [Inject]
    private PlayerAppearanceService _playerAppearanceService;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        
        else Destroy(gameObject);
    }

    [Rpc(SendTo.Server)]
    public void AddPlayerAppearanceRpc(PlayerAppearanceData playerAppearance)
    {
        PlayerAppearances.Add(playerAppearance);
        
        SyncPlayerAppearancesRpc();
    }
    
    IEnumerator SyncPlayerAppearances()
    {
        while (PlayerAppearances.Count != NetworkManager.ConnectedClients.Count)
        {
            yield return null;
        }
        
        _playerAppearanceService.SyncPlayerAppearances();
    }

    [Rpc(SendTo.Everyone)]
    private void SyncPlayerAppearancesRpc()
    {
        StartCoroutine(SyncPlayerAppearances());
    }
}