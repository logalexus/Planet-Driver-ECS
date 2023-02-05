using Codebase.Assets;
using Codebase.Content.Cars;
using Codebase.Content.Maps;
using Codebase.Controllers;
using Codebase.Data;
using Codebase.ECS.Code.EcsWorld;
using Codebase.Pools;
using Codebase.UI;
using UI.Popups;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private UIService uiService;
        [SerializeField] private PopupService popupService;
        [SerializeField] private PlayerService playerService;
        [SerializeField] private PlanetService planetService;
        [SerializeField] private CarsService carsService;
        [SerializeField] private AudioService audioService;
        [SerializeField] private PoolService poolService;

        public override void InstallBindings()
        {
            Container.Bind<UIService>().FromInstance(uiService).AsSingle();
            Container.Bind<PopupService>().FromInstance(popupService).AsSingle();
            Container.Bind<PlayerService>().FromInstance(playerService).AsSingle();
            Container.Bind<PlanetService>().FromInstance(planetService).AsSingle();
            Container.Bind<CarsService>().FromInstance(carsService).AsSingle();
            Container.Bind<PoolService>().FromInstance(poolService).AsSingle();
            Container.Bind<DataService>().AsSingle();
            Container.Bind<InstantiateService>().AsSingle();
            
            
            Container.BindInterfacesAndSelfTo<AudioService>().FromInstance(audioService).AsSingle();
        }
    }
}