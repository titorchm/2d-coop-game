using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "ScriptableObjects/Abilities/Fireball")]
public class Fireball : Ability
{
    public float fireballDamage;
    public float fireballSpeed;
    public float fireballRadius;
    
    public override void Activate(GameObject user)
    {
        throw new System.NotImplementedException();
    }
}
