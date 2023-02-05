using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace ECS.Code.Inputs
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct InputComponent : IComponent 
    {
        public float Vertical;
        public float Horizontal;
    }
}