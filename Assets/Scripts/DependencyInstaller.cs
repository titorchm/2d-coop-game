using Zenject;

public class DependencyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SessionService>().AsSingle().NonLazy();
        Container.Bind<SceneService>().AsSingle().NonLazy();
    }
}