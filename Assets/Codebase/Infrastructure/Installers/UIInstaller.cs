using System;
using Codebase.UI.Screens;
using Codebase.ZyphUI;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuScreen mainMenuScreen;
        [SerializeField] private CarsScreen carsScreen;
        [SerializeField] private InGameScreen inGameScreen;
        [SerializeField] private LoseScreen loseScreen;
        [SerializeField] private PlanetsScreen planetsScreen;
        [SerializeField] private SettingsScreen settingsScreen;
        [SerializeField] private StoreScreen storeScreen;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MainMenuScreen>().FromInstance(mainMenuScreen);
            Container.BindInterfacesTo<CarsScreen>().FromInstance(carsScreen);
            Container.BindInterfacesTo<InGameScreen>().FromInstance(inGameScreen);
            Container.BindInterfacesTo<LoseScreen>().FromInstance(loseScreen);
            Container.BindInterfacesTo<PlanetsScreen>().FromInstance(planetsScreen);
            Container.BindInterfacesTo<SettingsScreen>().FromInstance(settingsScreen);
            Container.BindInterfacesTo<StoreScreen>().FromInstance(storeScreen);
        }
    }
}