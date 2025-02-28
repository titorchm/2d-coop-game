using UnityEngine;

public interface IBlock
{
}

[CreateAssetMenu(fileName = "BlockData", menuName = "ScriptableObjects/BlockData")]
public class BlockData : ScriptableObject, IBlock
{
    public string Name;
    public Sprite Sprite;
}
