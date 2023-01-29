using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/Planets" + nameof(InputInitializer))]
public sealed class InputInitializer : Initializer 
{
    public override void OnAwake()
    {
        var entity = this.World.CreateEntity();
        entity.AddComponent<InputComponent>();
    }
}