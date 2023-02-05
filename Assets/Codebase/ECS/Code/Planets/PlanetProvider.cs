using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace ECS.Code.Planets
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlanetProvider : MonoProvider<Planet> 
    {
        [SerializeField] private Transform startPosition;

        public Transform StartPosition => startPosition;
    }
}