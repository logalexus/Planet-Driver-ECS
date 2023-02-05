using Codebase.ECS.Code.Enemies;
using Codebase.ECS.Code.Timers;
using ECS.Code.Cars;
using ECS.Code.Enemies;
using ECS.Code.Inputs;
using ECS.Code.Planets;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Zenject;

namespace Codebase.ECS.Code.EcsWorld
{
    public class WorldInitializer
    {
        private World _world;
        private SystemsGroup _systemsGroup;
        private DiContainer _diContainer;

        public WorldInitializer(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void CreateWorld()
        {
            _world = World.Default;
            _world.UpdateByUnity = true;
            _systemsGroup = _world.CreateSystemsGroup();
            
            CreateOtherSystems();
            CreateEnemySystems();
            CreatePlayerSystems();

            _world.AddSystemsGroup(0, _systemsGroup);
        }

        private void CreateEnemySystems()
        {
            CreateSystem<EnemySpawnSystem>();
            CreateSystem<EnemyMovableSystem>();
            CreateSystem<EnemySpawnSystem>();
            CreateSystem<EnemyStartMovingSystem>();
            CreateSystem<EnemyStopMovingSystem>();
            CreateSystem<EnemyStartSpawnSystem>();
            CreateSystem<EnemyStopSpawnSystem>();
        }

        private void CreatePlayerSystems()
        {
            CreateInitializer<InputInitializer>();
            CreateSystem<InputSystem>();
            CreateSystem<PlayerCarMovableSystem>();
        }

        private void CreateOtherSystems()
        {
            CreateSystem<PlanetGravitySystem>();
            CreateSystem<TimerSystem>();
        }

        private void CreateSystem<T>() where T : ISystem, new()
        {
            ISystem system = new T();
            _systemsGroup.AddSystem(system);
            _diContainer.Inject(system);
        }
        
        private void CreateInitializer<T>() where T : IInitializer, new()
        {
            IInitializer initializer = new T();
            _systemsGroup.AddInitializer(initializer);
            _diContainer.Inject(initializer);
        }
    }
}