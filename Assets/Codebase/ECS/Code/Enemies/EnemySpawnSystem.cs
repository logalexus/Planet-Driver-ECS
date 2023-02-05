using Codebase.ECS.Code.Timers;
using Codebase.Pools;
using ECS.Code.Common;
using ECS.Code.Planets;
using ECS.Code.Players;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using Zenject;

namespace Codebase.ECS.Code.Enemies
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class EnemySpawnSystem : ISystem
    {
        public World World { get; set; }

        private Filter _spawners;
        private Filter _playerCar;
        private Filter _planet;
        private PoolService _poolService;


        [Inject]
        public void Init(PoolService poolService)
        {
            _poolService = poolService;
        }

        public void OnAwake()
        {
            _spawners = World.Filter.With<EnemySpawner>().With<TimerComplete>();
            _playerCar = World.Filter.With<Player>();
            _planet = World.Filter.With<Planet>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_planet.IsEmpty() || _playerCar.IsEmpty())
                return;

            Entity planetEntity = _planet.First();
            Planet planet = planetEntity.GetComponent<Planet>();
            Transform planetTransform = planetEntity.GetComponent<TransformComponent>().Transform;
            Transform playerCar = _playerCar.First().GetComponent<TransformComponent>().Transform;

            float diameterPlanet = planet.Collider.radius * planetTransform.transform.localScale.x * 2;

            foreach (var spawnerEntity in _spawners)
            {
                EnemySpawner spawner = spawnerEntity.GetComponent<EnemySpawner>();
                Vector3 pos = playerCar.transform.position - playerCar.transform.up * (diameterPlanet);
                Quaternion rot = playerCar.transform.rotation * Quaternion.Euler(180f, 180f, 0) *
                                 Quaternion.Euler(0, Random.Range(-90, 90), 0);

                if (!Physics.CheckSphere(pos, 5f, spawner.Mask))
                {
                    var enemy = _poolService.EnemiesPool.GetFreeElement();
                    enemy.transform.position = pos;
                    enemy.transform.rotation = rot;
                }

                spawnerEntity.RemoveComponent<TimerComplete>();
            }
        }

        public void Dispose()
        {
        }
    }
}