using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

namespace ECS.Code.Players
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class PlayerProvider : MonoProvider<Player>
    {
        
    }
}