using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace ECS.Code.Inputs
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class InputInitializer : IInitializer 
    {
        public World World { get; set; }
        
        public void OnAwake()
        {
            var entity = this.World.CreateEntity();
            entity.AddComponent<InputComponent>();
        }

        public void Dispose()
        {
            
        }
    }
}