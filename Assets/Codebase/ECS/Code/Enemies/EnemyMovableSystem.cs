using ECS.Code.Cars;
using ECS.Code.Common;
using ECS.Code.Inputs;
using ECS.Code.Players;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Codebase.ECS.Code.Enemies
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class EnemyMovableSystem : IFixedSystem
    {
        public World World { get; set; }

        private Filter _input;
        private Filter _cars;

        public void OnAwake()
        {
            _cars = World.Filter.With<Car>().With<Enemy>().Without<EnemyStopMoving>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var car in _cars)
            {
                ref var rigidbody = ref car.GetComponent<RigidbodyComponent>().Rigidbody;
                var transform = car.GetComponent<TransformComponent>().Transform;
                var speed = car.GetComponent<Car>().Speed;

                rigidbody.MovePosition(rigidbody.position + transform.forward * speed * deltaTime);
            }
        }

        public void Dispose()
        {
        }
    }
}