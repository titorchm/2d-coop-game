using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public string abilityDescription;
    public Sprite abilityIcon;
    
    public abstract void Activate(GameObject user);
}
