using System;
using Codebase.ECS.Code.Timers;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

namespace Codebase.ECS.Code.Enemies
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class EnemySpawnerProvider : MonoProvider<EnemySpawner>
    {
        private void Awake()
        {
            this.Entity.AddComponent<TimerStop>();
        }
    }
}