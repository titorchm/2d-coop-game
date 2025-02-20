using Unity.Netcode;
using Zenject;
using Services;

public class DependencyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // Services
        Container.Bind<SessionStartService>().AsSingle().NonLazy();
        Container.Bind<SceneManagementService>().AsSingle().NonLazy();
        Container.Bind<PlayerAppearanceService>().AsSingle().NonLazy();
        
        // MonoBehaviours
        Container.Bind<ClientConnectionHandler>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<NetworkManager>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}