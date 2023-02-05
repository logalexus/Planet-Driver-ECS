using ECS.Code.Cars;
using ECS.Code.Common;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Codebase.ECS.Code.Enemies
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class EnemyStartMovingSystem : IFixedSystem
    {
        public World World { get; set; }

        private Filter _event;
        private Filter _cars;

        public void OnAwake()
        {
            _event = World.Filter.With<EnemyStartMovingEvent>();
            _cars = World.Filter.With<Enemy>().With<EnemyStopMoving>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_event.IsEmpty())
                return;

            foreach (var carEntity in _cars)
                carEntity.RemoveComponent<EnemyStopMoving>();

            foreach (var eventEntity in _event)
                eventEntity.RemoveComponent<EnemyStartMovingEvent>();
        }

        public void Dispose()
        {
        }
    }
}