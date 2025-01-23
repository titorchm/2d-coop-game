using UnityEngine;

[CreateAssetMenu(fileName = "Immunity", menuName = "ScriptableObjects/Abilities/Immunity")]
public class Immunity : Ability
{
    public float immunityTime = 0.2f;
    public float immunityCooldown = .2f;
}
