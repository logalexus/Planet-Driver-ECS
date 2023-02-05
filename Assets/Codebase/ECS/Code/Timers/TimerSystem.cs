using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Codebase.ECS.Code.Timers
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class TimerSystem : ISystem
    {
        public World World { get; set; }

        private Filter _timers;

        public void OnAwake()
        {
            _timers = World.Filter.With<Timer>().Without<TimerStop>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var timerEntity in _timers)
            {
                ref Timer timer = ref timerEntity.GetComponent<Timer>();

                if (timer.CurrentTime >= timer.Duration)
                {
                    timer.CurrentTime = 0;
                    timerEntity.AddComponent<TimerComplete>();
                    break;
                }

                timer.CurrentTime += deltaTime;
            }
        }

        public void Dispose()
        {
        }
    }
}