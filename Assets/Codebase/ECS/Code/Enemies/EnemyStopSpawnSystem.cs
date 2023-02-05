using Codebase.ECS.Code.Enemies;
using Codebase.ECS.Code.Timers;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace ECS.Code.Enemies
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class EnemyStopSpawnSystem : ISystem
    {
        public World World { get; set; }

        private Filter _timers;
        private Filter _event;

        public void OnAwake()
        {
            _timers = World.Filter.With<Timer>().Without<TimerStop>().With<EnemySpawner>();
            _event = World.Filter.With<EnemyStopSpawnEvent>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_event.IsEmpty())
                return;

            foreach (var timerEntity in _timers)
                timerEntity.AddComponent<TimerStop>();

            foreach (var eventEntity in _event)
                eventEntity.RemoveComponent<EnemyStopSpawnEvent>();
        }

        public void Dispose()
        {
        }
    }
}