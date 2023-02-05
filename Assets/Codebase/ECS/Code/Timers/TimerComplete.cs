using System;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Codebase.ECS.Code.Timers
{
    [Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct TimerComplete : IComponent
    {
        
    }
}