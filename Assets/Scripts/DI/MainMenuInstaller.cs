using Zenject;
using Services;

public class MainMenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // Services
        Container.Bind<SessionStartService>().AsSingle().NonLazy();
        
        // MonoBehaviours
        Container.Bind<ClientConnectionHandler>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}