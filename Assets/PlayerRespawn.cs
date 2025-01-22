using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public void RespawnPlayer(Component sender, object data)
    {
        if (data is Transform)
        {
            Transform player = (Transform)data;
            
            player.position = transform.position;
        }
    }
}
