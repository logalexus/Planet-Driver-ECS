using System.Linq;
using Codebase.UI.Screens;
using Codebase.ZyphUI.Base;

namespace Codebase.UI
{
    public class UIService : BaseUIController
    {
        public void OpenMainMenuScreen()
        {
            var screen = _screens.OfType<MainMenuScreen>().First();
            OpenScreenWithCloseAll(screen);
        }

        public void OpenCarsScreen()
        {
            var screen = _screens.OfType<CarsScreen>().First();
            OpenScreenOverAll(screen);
        }
        
        public void OpenInGameScreen()
        {
            var screen = _screens.OfType<InGameScreen>().First();
            OpenScreenWithCloseAll(screen);
        }
        
        public void OpenLoseScreen()
        {
            var screen = _screens.OfType<LoseScreen>().First();
            OpenScreenWithCloseAll(screen);
        }
        
        public void OpenPlanetsScreen()
        {
            var screen = _screens.OfType<PlanetsScreen>().First();
            OpenScreenOverAll(screen);
        }
        
        public void OpenSettingsScreen()
        {
            var screen = _screens.OfType<SettingsScreen>().First();
            OpenScreenOverAll(screen);
        }
        
        public void OpenStoreScreen()
        {
            var screen = _screens.OfType<StoreScreen>().First();
            OpenScreenOverAll(screen);
        }
    }
}