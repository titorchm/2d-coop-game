using UnityEngine;
using Zenject;

public class MainWorldInstaller : MonoInstaller
{
    [SerializeField] private PlayerInput playerInput;
    
    public override void InstallBindings()
    {
        //Container.BindInstance(playerInput).AsSingle().NonLazy();
        Container.Bind<PlayerInput>().FromInstance(playerInput).AsSingle().NonLazy();
    }
}