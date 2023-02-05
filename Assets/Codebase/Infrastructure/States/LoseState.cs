using Codebase.Controllers;
using Codebase.Spawners;
using Codebase.UI;
using Zenject;

namespace Codebase.Infrastructure.States
{
    public class LoseState : IState
    {
        private LazyInject<GameMachine> _gameMachine;
        private UIService _uiService;
        private PlayerService _playerService;
        private CinemachineSwitcher _cinemachineSwitcher;

        public LoseState(LazyInject<GameMachine> gameMachine, UIService uiService, PlayerService playerService,
            CinemachineSwitcher cinemachineSwitcher)
        {
            _gameMachine = gameMachine;
            _uiService = uiService;
            _playerService = playerService;
            _cinemachineSwitcher = cinemachineSwitcher;
        }

        public void Enter()
        {
            _uiService.OpenLoseScreen();
            _cinemachineSwitcher.SwitchAroundCamera();
        }

        public void Exit()
        {
            _playerService.RestartPlayer();
        }
    }
}