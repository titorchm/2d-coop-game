using Unity.Netcode;
using Zenject;
using Services;
using UnityEngine;

public class ProjectDependencyInstaller : MonoInstaller
{
    [SerializeField] private PlayerAppearance playerAppearance;
    
    [SerializeField] private AppearanceData appearanceData;
    
    private NetworkManager _networkManager;
    
    public override void InstallBindings()
    {
        _networkManager = FindFirstObjectByType<NetworkManager>();
        
        // Services
        Container.Bind<SceneManagementService>().AsSingle().NonLazy();
        Container.Bind<PlayerAppearanceService>().AsSingle().NonLazy();
        
        // MonoBehaviours
        Container.Bind<NetworkManager>().FromInstance(_networkManager).AsSingle().NonLazy();
        
        // ScriptableObjects
        Container.BindInstance(appearanceData).AsSingle().NonLazy();
        Container.BindInstance(playerAppearance).AsSingle().NonLazy();
    }
    
}