using UnityEngine;

[CreateAssetMenu(fileName = "InteractableObject", menuName = "ScriptableObjects/InteractableObject/Item")]
public class Item : InteractableObject
{
    public Sprite sprite;
    public int amount;
    
    public GameEvent OnItemPickUp;
    
    public override void Interact()
    {
        Debug.Log("Interact");
        OnItemPickUp.Raise(this);
    }
}
