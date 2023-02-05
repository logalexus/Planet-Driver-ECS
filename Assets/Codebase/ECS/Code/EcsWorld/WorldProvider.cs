using Codebase.ECS.Code.Enemies;
using Scellecs.Morpeh;

namespace Codebase.ECS.Code.EcsWorld
{
    public class WorldProvider
    {
        public void StartEnemyMoving()
        {
            World.Default.CreateEntity()
                .AddComponent<EnemyStartMovingEvent>();
        }
        
        public void StopEnemyMoving()
        {
            World.Default.CreateEntity()
                .AddComponent<EnemyStopMovingEvent>();
        }
        
        public void StartEnemySpawning()
        {
            World.Default.CreateEntity()
                .AddComponent<EnemyStartSpawnEvent>();
        }
        
        public void StopEnemySpawning()
        {
            World.Default.CreateEntity()
                .AddComponent<EnemyStopSpawnEvent>();
        }
    }
}