using Codebase.ECS.Code.Enemies;
using UnityEngine;
using Zenject;

namespace Codebase.Configs
{
    [CreateAssetMenu(fileName = "Config", menuName = "SO/Config", order = 0)]
    public class Config : ScriptableObjectInstaller
    {
        [SerializeField] private EnemiesSettings enemiesSettings;
        [SerializeField] private CoinsSettings coinsSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstances(enemiesSettings, coinsSettings);
        }
    }
}