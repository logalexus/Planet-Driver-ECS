using System;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Codebase.ECS.Code.Coins
{
    [Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct Coin : IComponent
    {
        
    }
}