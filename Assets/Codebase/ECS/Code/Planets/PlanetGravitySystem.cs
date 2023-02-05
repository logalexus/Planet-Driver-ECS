using ECS.Code.Common;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace ECS.Code.Planets
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlanetGravitySystem : IFixedSystem
    {
        public World World { get; set; }
        
        private Filter _planets;
        private Filter _bodies;

        public void OnAwake()
        {
            _planets = this.World.Filter.With<Planet>()
                .With<GravityComponent>();

            _bodies = this.World.Filter.With<RigidbodyComponent>();
        }


        public void OnUpdate(float deltaTime)
        {
            foreach (var planetEntity in _planets)
            {
                ref Planet planet = ref planetEntity.GetComponent<Planet>();
                ref GravityComponent gravityComponent = ref planetEntity.GetComponent<GravityComponent>();

                foreach (var bodyEntity in _bodies)
                {
                    ref Rigidbody body = ref bodyEntity.GetComponent<RigidbodyComponent>().Rigidbody;

                    Vector3 center = planet.Collider.bounds.center;
                    float power = (float)(gravityComponent.G * body.mass * planet.Mass);
                    Vector3 force = power * (center - body.transform.position).normalized;
                    force /= (center - body.transform.position).sqrMagnitude;
                    body.AddForce(force, ForceMode.Force);
                }
            }
        }

        public void Dispose()
        {
            
        }
    }
}