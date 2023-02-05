using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codebase.Infrastructure.States;
using Zenject;

namespace Codebase.Infrastructure
{
    public class GameMachine : IInitializable
    {
        private List<IState> _states;
        private IState _activeState;

        public GameMachine(List<IState> states)
        {
            _states = states;
        }

        public void Initialize()
        {
            Enter<BootstrapState>();
        }

        public void Enter<T>() where T : IState
        {
            _activeState?.Exit();
            _activeState = _states.OfType<T>().First();
            _activeState.Enter();
        }
    }
}