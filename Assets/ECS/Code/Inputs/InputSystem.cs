using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/Inputs" + nameof(InputSystem))]
public sealed class InputSystem : UpdateSystem
{
    private Filter _inputs;
    private Filter _joysticks;

    public override void OnAwake()
    {
        _inputs = this.World.Filter.With<InputComponent>();
        _joysticks = this.World.Filter.With<JoystickComponent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var input in _inputs)
        {
            ref InputComponent inputComponent = ref input.GetComponent<InputComponent>();

            foreach (var joystick in _joysticks)
            {
                ref JoystickComponent joystickComponent = ref joystick.GetComponent<JoystickComponent>();

                inputComponent.Vertical = joystickComponent.Joystick.Vertical;
                inputComponent.Horizontal = joystickComponent.Joystick.Horizontal;
            }
        }
    }
}