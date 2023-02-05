using Codebase.Controllers;
using Codebase.Data;
using Codebase.UI;
using UnityEngine.Rendering.PostProcessing;
using Zenject;

namespace Codebase.Infrastructure.States
{
    public class MenuState : IState
    {
        private LazyInject<GameMachine> _gameMachine;
        private UIService _uiService;

        public MenuState(LazyInject<GameMachine> gameMachine, UIService uiService)
        {
            _gameMachine = gameMachine;
            _uiService = uiService;
        }

        public void Enter()
        {
            _uiService.OpenMainMenuScreen();
        }

        public void Exit()
        {
        }
    }
}