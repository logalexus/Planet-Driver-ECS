using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/Cars" + nameof(CarMovableSystem))]
public sealed class CarMovableSystem : FixedUpdateSystem
{
    private Filter _input;
    private Filter _cars;

    public override void OnAwake()
    {
        _input = this.World.Filter.With<InputComponent>();
        _cars = this.World.Filter.With<CarComponent>()
            .With<RigidbodyComponent>()
            .With<ControllableComponent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        if (_input.IsEmpty())
            return;

        ref InputComponent inputComponent = ref _input.First().GetComponent<InputComponent>();

        foreach (var car in _cars)
        {
            ref var carComponent = ref car.GetComponent<CarComponent>();
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
}