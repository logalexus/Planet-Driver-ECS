using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace ECS.Code.Inputs
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class InputSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _inputs;
        private Filter _joysticks;

        public void OnAwake()
        {
            _inputs = this.World.Filter.With<InputComponent>();
            _joysticks = this.World.Filter.With<JoystickComponent>();
        }


        public void OnUpdate(float deltaTime)
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

        public void Dispose()
        {
            
        }
    }
}