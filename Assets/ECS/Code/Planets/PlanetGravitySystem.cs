using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/Planets" + nameof(PlanetGravitySystem))]
public sealed class PlanetGravitySystem : FixedUpdateSystem
{
    private Filter _planets;
    private Filter _bodies;

    public override void OnAwake()
    {
        _planets = this.World.Filter.With<PlanetComponent>()
            .With<GravityComponent>();

        _bodies = this.World.Filter.With<RigidbodyComponent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var planetEntity in _planets)
        {
            ref PlanetComponent planetComponent = ref planetEntity.GetComponent<PlanetComponent>();
            ref GravityComponent gravityComponent = ref planetEntity.GetComponent<GravityComponent>();

            foreach (var bodyEntity in _bodies)
            {
                ref Rigidbody body = ref bodyEntity.GetComponent<RigidbodyComponent>().Rigidbody;

                Vector3 center = planetComponent.Collider.bounds.center;
                float power = (float)(gravityComponent.G * body.mass * planetComponent.Mass);
                Vector3 force = power * (center - body.transform.position).normalized;
                force /= (center - body.transform.position).sqrMagnitude;
                body.AddForce(force, ForceMode.Force);
            }
        }
    }
}