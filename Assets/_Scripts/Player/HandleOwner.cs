using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class HandleOwner : NetworkBehaviour
{
    [SerializeField] private MonoBehaviour[] scripts;
    [SerializeField] private CinemachineVirtualCameraBase vc;
    [SerializeField] private AudioListener audioListener;
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner)
        {
            foreach (var script in scripts)
            {
                script.enabled = true;
            }
            
            vc.Priority = 1;
            audioListener.enabled = true;
        }
    }
}
