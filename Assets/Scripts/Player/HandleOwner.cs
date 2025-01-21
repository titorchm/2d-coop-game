using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class HandleOwner : NetworkBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private CinemachineVirtualCameraBase vc;
    [SerializeField] private AudioListener audioListener;
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner)
        {
            vc.Priority = 1;
            playerMovement.enabled = true;
            audioListener.enabled = true;
        }
    }
}
