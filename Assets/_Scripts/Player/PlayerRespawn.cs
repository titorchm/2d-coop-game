using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public void RespawnPlayer(object data)
    {
        if (data is Transform)
        {
            Transform player = (Transform)data;
            
            player.position = transform.position;
        }
    }
}
