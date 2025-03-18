using NUnit.Framework.Constraints;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float playerWidth = 2f;
    public float moveSpeed = 5f;
    public float shiftSpeed = 10f;
    public float jumpForce = 15f;
}
