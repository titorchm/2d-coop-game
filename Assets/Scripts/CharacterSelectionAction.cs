using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectionAction : MonoBehaviour
{
    [SerializeField] private GameEvent onCharacterSelectionOpen;
    
    public Button button;
    
    void Start()
    {
        button.onClick.AddListener(ChangeCharacter);
    }

    private void ChangeCharacter()
    {
        CustomSceneManager.Instance.LoadSceneAsync(SceneNames.CharacterSelection, LoadSceneMode.Additive);
    }
}
