using Unity.Netcode;
using UnityEngine;

public class GenerateGround : NetworkBehaviour
{
    [SerializeField] private GameObject groundPrefab;

    void Start()
    {
        if(!(IsServer || IsHost)) return; 
        
        InstantiateGroundRpc();
    }

    private void InstantiateGround()
    {
        for (int i = 0; i < 100; i++)
        {
            var instance = Instantiate(groundPrefab, new Vector2(i, -5f), Quaternion.identity);
            var instanceNetworkObject = instance.GetComponent<NetworkObject>();
            instanceNetworkObject.Spawn();
        }
    }
    
    [Rpc(SendTo.Everyone)]
    private void InstantiateGroundRpc()
    {
        InstantiateGround();
    }
}
