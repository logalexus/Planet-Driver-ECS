using Codebase.Assets;
using Codebase.ECS.Code.Coins;
using Codebase.ECS.Code.Enemies;
using UnityEngine;
using Zenject;

namespace Codebase.Pools
{
    public class PoolService : MonoBehaviour
    {
        [SerializeField] private int enemyCount;
        [SerializeField] private int coinsCount;
        [SerializeField] private Transform enemyContainer;
        [SerializeField] private Transform coinContainer;

        private AssetsProvider _assetsProvider;
        private PoolObjects<EnemyProvider> _enemiesPool;
        private PoolObjects<CoinProvider> _coinsPool;

        public PoolObjects<EnemyProvider> EnemiesPool => _enemiesPool;

        public PoolObjects<CoinProvider> CoinsPool => _coinsPool;

        [Inject]
        public void Init(AssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public void CreatePools()
        {
            _enemiesPool = new PoolObjects<EnemyProvider>(_assetsProvider.Enemies, enemyCount, enemyContainer);
            _coinsPool = new PoolObjects<CoinProvider>(_assetsProvider.Coins, coinsCount, coinContainer);
        }
    }
}