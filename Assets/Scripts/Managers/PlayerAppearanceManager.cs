using System;
using System.Collections;
using System.Collections.Generic;
using Services;
using Unity.Netcode;
using UnityEngine;
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

        StartCoroutine(SyncPlayerAppearances());
    }

    IEnumerator SyncPlayerAppearances()
    {
        // wait for .1 second to fully sync the count
        yield return new WaitForSeconds(0.1f);
        
        SyncPlayerAppearancesRpc();
    }

    [Rpc(SendTo.Everyone)]
    private void SyncPlayerAppearancesRpc()
    {
        _playerAppearanceService.SyncPlayerAppearances();
    }
}