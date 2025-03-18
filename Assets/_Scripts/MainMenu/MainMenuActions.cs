using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuActions : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuActions;
    
    [SerializeField] private Button singlePlayer;
    [SerializeField] private Button multiplayer;
    [SerializeField] private Button characterCustomize;
    [SerializeField] private Button gameOptions;
    [SerializeField] private Button quitGame;
    
    [SerializeField] private Button backToMainMenu;
    
    [SerializeField] private GameEvent onMenuActionPressed;
    [SerializeField] private GameEvent onBackToMainMenuPressed;
    
    private void Awake()
    {
        singlePlayer.onClick.AddListener(HandleSinglePlayerClick);
        multiplayer.onClick.AddListener(HandleMultiplayerClick);
        characterCustomize.onClick.AddListener(HandleCharacterCustomizeClick);
        gameOptions.onClick.AddListener(HandleGameOptionsClick);
        quitGame.onClick.AddListener(HandleQuitGameClick);
        
        backToMainMenu.onClick.AddListener(HandleBackToMainMenuClick);
    }

    private void HandleSinglePlayerClick()
    {
        mainMenuActions.SetActive(false);
        
        onMenuActionPressed.Raise(MenuActions.SinglePlayer);
        
        backToMainMenu.gameObject.SetActive(true);
    }

    private void HandleMultiplayerClick()
    {
        mainMenuActions.SetActive(false);
        
        onMenuActionPressed.Raise(MenuActions.Multiplayer);
        
        backToMainMenu.gameObject.SetActive(true);
    }
    
    private void HandleCharacterCustomizeClick()
    {
        mainMenuActions.SetActive(false);
        
        onMenuActionPressed.Raise(MenuActions.Character);
        
        backToMainMenu.gameObject.SetActive(true);
    }

    private void HandleGameOptionsClick()
    {
        mainMenuActions.SetActive(false);
        
        onMenuActionPressed.Raise(MenuActions.Options);
        
        backToMainMenu.gameObject.SetActive(true);
    }
    
    private void HandleQuitGameClick()
    {
        Application.Quit();
    }
    
    private void HandleBackToMainMenuClick()
    {
        mainMenuActions.SetActive(true);
        
        onBackToMainMenuPressed.Raise(null);
        
        backToMainMenu.gameObject.SetActive(false);
    }
}

public enum MenuActions
{
    SinglePlayer,
    Multiplayer,
    Character,
    Options
}