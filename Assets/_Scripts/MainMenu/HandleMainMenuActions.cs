using UnityEngine;

public class HandleMainMenuActions : MonoBehaviour
{
    [SerializeField] private GameObject singlePlayerPanel;
    [SerializeField] private GameObject multiplayerPanel;
    [SerializeField] private GameObject characterPanel;
    [SerializeField] private GameObject optionsPanel;

    public void HandleMenuAction(object menuAction)
    {
        switch ((MenuActions)menuAction)
        {
            case MenuActions.SinglePlayer:
                EnableSinglePlayerPanel();
                break;
            case MenuActions.Multiplayer:
                EnableMultiplayerPanel();
                break;
            case MenuActions.Character:
                EnableCharacterPanel();
                break;
            case MenuActions.Options:
                EnableOptionsPanel();
                break;
        }
    }
    
    public void DisableAllPanels(object data)
    {
        singlePlayerPanel.SetActive(false);
        multiplayerPanel.SetActive(false);
        characterPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }
    
    private void EnableSinglePlayerPanel()
    {
        singlePlayerPanel.SetActive(true);
    }

    private void EnableMultiplayerPanel()
    {
        multiplayerPanel.SetActive(true);
    }

    private void EnableCharacterPanel()
    {
        characterPanel.SetActive(true);
    }

    private void EnableOptionsPanel()
    {
        optionsPanel.SetActive(true);
    }
}
