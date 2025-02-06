using UnityEngine;

public abstract class InteractableObject : ScriptableObject
{
    public string objectName;
    
    public abstract void Interact();
}
