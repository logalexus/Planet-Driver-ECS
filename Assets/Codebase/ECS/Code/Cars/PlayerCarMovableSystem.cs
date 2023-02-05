using ECS.Code.Common;
using ECS.Code.Inputs;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace ECS.Code.Cars
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerCarMovableSystem : IFixedSystem
    {
        public World World { get; set; }
        
        private Filter _input;
        private Filter _cars;

        public void OnAwake()
        {
            _input = World.Filter.With<InputComponent>();
            _cars = World.Filter.With<Car>()
                .With<RigidbodyComponent>()
                .With<ControllableComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_input.IsEmpty())
                return;

            ref InputComponent inputComponent = ref _input.First().GetComponent<InputComponent>();

            foreach (var car in _cars)
            {
                ref var carComponent = ref car.GetComponent<Car>();
                ref var rigidbodyComponent = ref car.GetComponent<RigidbodyComponent>();
                ref var transformComponent = ref car.GetComponent<TransformComponent>();

                Rigidbody rigidbody = rigidbodyComponent.Rigidbody;
                Transform transform = transformComponent.Transform;
                float speed = carComponent.Speed;
                float rotationSpeed = carComponent.RotationSpeed;
                float verticalStrength = carComponent.VerticalStrength;

                rigidbody.MovePosition(rigidbody.position +
                                       transform.forward *
                                       (speed + inputComponent.Vertical * verticalStrength) * deltaTime);

                Vector3 yRotation = Vector3.up * inputComponent.Horizontal * rotationSpeed * deltaTime;
                Quaternion deltaRotation = Quaternion.Euler(yRotation);
                Quaternion targetRotation = rigidbody.rotation * deltaRotation;
                rigidbody.MoveRotation(Quaternion.Slerp(rigidbody.rotation, targetRotation, 100f * deltaTime));
            }
        }

        public void Dispose()
        {
            
        }
    }
}