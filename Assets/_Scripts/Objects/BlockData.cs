using Unity.VisualScripting;
using UnityEngine;

public interface IItem
{
    string Name { get; set; }
    Sprite Sprite { get; set; }
}

[CreateAssetMenu(fileName = "BlockData", menuName = "ScriptableObjects/BlockData")]
public class BlockData : ScriptableObject, IItem
{
    [SerializeField] private string blockName;
    [SerializeField] private Sprite sprite;
    
    public string Name 
    { 
        get => blockName; 
        set => blockName = value; 
    }
    
    public Sprite Sprite 
    { 
        get => sprite; 
        set => sprite = value; 
    }
}
