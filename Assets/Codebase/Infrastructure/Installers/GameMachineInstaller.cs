using Codebase.Infrastructure.States;
using UnityEditor;
using Zenject;

namespace Codebase.Infrastructure.Installers
{
    public class GameMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle().WhenInjectedInto<GameMachine>();
            Container.BindInterfacesAndSelfTo<InGameState>().AsSingle().WhenInjectedInto<GameMachine>();
            Container.BindInterfacesAndSelfTo<LoseState>().AsSingle().WhenInjectedInto<GameMachine>();
            Container.BindInterfacesAndSelfTo<MenuState>().AsSingle().WhenInjectedInto<GameMachine>();
            
            Container.BindInterfacesAndSelfTo<GameMachine>().AsSingle();
        }
    }
}