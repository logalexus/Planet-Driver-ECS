using Content.Joystick_Pack.Scripts.Base;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace ECS.Code.Inputs
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct JoystickComponent : IComponent 
    {
        public Joystick Joystick;
    }
}