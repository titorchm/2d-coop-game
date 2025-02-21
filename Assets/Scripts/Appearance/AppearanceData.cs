using UnityEngine;

[CreateAssetMenu(fileName = "AppearanceData", menuName = "Scriptable Objects/AppearanceData")]
public class AppearanceData : ScriptableObject
{
    public Sprite[] body;
    public Sprite[] face;
    public Sprite[] eyes;
    public Sprite[] hat;
}
