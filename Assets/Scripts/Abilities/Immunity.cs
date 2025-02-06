using UnityEngine;

[CreateAssetMenu(fileName = "Immunity", menuName = "ScriptableObjects/Abilities/Immunity")]
public class Immunity : Ability
{
    public float immunityTime;
    public float immunityCooldown;
    
    public override void Activate(GameObject user)
    {
        throw new System.NotImplementedException();
    }
}
