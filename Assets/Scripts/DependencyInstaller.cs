using Unity.Netcode;
using Zenject;
using Services;
using UnityEngine;

public class DependencyInstaller : MonoInstaller
{
    [SerializeField] private AppearanceData appearanceData;
    
    public override void InstallBindings()
    {
        // Services
        Container.Bind<SessionStartService>().AsSingle().NonLazy();
        Container.Bind<SceneManagementService>().AsSingle().NonLazy();
        Container.Bind<PlayerAppearanceService>().AsSingle().NonLazy();
        
        // MonoBehaviours
        Container.Bind<ClientConnectionHandler>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<NetworkManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        
        // ScriptableObjects
        Container.BindInstance(appearanceData).AsSingle().NonLazy();
    }
}