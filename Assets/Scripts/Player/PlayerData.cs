using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float moveSpeed = 5f;
    public float jumpForce = 15f;
    public float dashForce = 15f;
    public float dashTime = 0.5f;
}
