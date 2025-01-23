using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "ScriptableObjects/Abilities/Dash")]
public class Dash : Ability
{
    public float dashForce = 15f;
    public float dashTime = 0.2f;
    public float dashCooldown = .2f;
}
