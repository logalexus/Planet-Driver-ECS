using Codebase.ECS.Code.EcsWorld;
using Codebase.Spawners;
using Codebase.UI;
using Zenject;

namespace Codebase.Infrastructure.States
{
    public class InGameState : IState
    {
        private LazyInject<GameMachine> _gameMachine;
        private UIService _uiService;
        private CinemachineSwitcher _cinemachineSwitcher;
        private WorldProvider _worldProvider;

        public InGameState(LazyInject<GameMachine> gameMachine, UIService uiService,
            CinemachineSwitcher cinemachineSwitcher, WorldProvider worldProvider)
        {
            _gameMachine = gameMachine;
            _uiService = uiService;
            _cinemachineSwitcher = cinemachineSwitcher;
            _worldProvider = worldProvider;
        }

        public void Enter()
        {
            _uiService.OpenInGameScreen();
            _cinemachineSwitcher.SwitchPlayingCamera();
            _worldProvider.StartEnemyMoving();
            _worldProvider.StartEnemySpawning();
        }

        public void Exit()
        {
            _worldProvider.StopEnemyMoving();
            _worldProvider.StopEnemySpawning();
        }
    }
}