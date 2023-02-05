using Codebase.Assets;
using Codebase.ECS.Code.EcsWorld;
using Codebase.Spawners;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Zenject;

namespace Codebase.Infrastructure.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private CinemachineSwitcher cinemachineSwitcher;
        [SerializeField] private AssetsProvider assetsProvider;
        [SerializeField] private PostProcessVolume postProcessVolume;

        public override void InstallBindings()
        {
            Container.Bind<CinemachineSwitcher>().FromInstance(cinemachineSwitcher).AsSingle();
            Container.Bind<AssetsProvider>().FromInstance(assetsProvider).AsSingle();
            Container.Bind<PostProcessVolume>().FromInstance(postProcessVolume).AsSingle();
            Container.Bind<WorldInitializer>().AsSingle();
            Container.Bind<WorldProvider>().AsSingle();
        }
    }
}