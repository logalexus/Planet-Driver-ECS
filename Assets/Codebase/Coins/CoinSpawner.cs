using System.Collections;
using System.Collections.Generic;
using Codebase.Controllers;
using Codebase.EmtyComponents;
using UnityEngine;

namespace Codebase.Spawners
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private SphereCollider _planet;
        [SerializeField] private Transform _car;
        [SerializeField] private List<Coin> _coinPrefab;
        [SerializeField] private LayerMask _mask;
        
        [Header("Config")] 
        [SerializeField] private int _poolCount = 10;
        [SerializeField] private float _frequence = 0.5f;

        private PoolObjects<Coin> _pool;
        private float _radiusPlanet;
        private bool _isPlaying = false;
        private Coroutine _spawnRoutine;

        private const float GROUND_OFFSET = 2f;

        private void Start()
        {
            _radiusPlanet = _planet.radius * _planet.transform.localScale.x;
            _pool = new PoolObjects<Coin>(_coinPrefab, _poolCount, transform);
        }

        public void StartSpawning()
        {
            _spawnRoutine = StartCoroutine(Spawn());
        }

        public void StopSpawning()
        {
            StopCoroutine(_spawnRoutine);
        }

        public void ResetSpawner()
        {
            _pool.DeactivateAll();
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(_frequence);

                Vector3 offset = Vector3.right * Random.Range(-20f, 20f);
                Vector3 pos = _planet.transform.position + (_planet.transform.position - _car.position).normalized *
                    (_radiusPlanet + GROUND_OFFSET);
                Vector3 offsetPos = pos + offset;
                Vector3 offsetPosNormal = _planet.transform.position +
                                          (offsetPos - _planet.transform.position).normalized *
                                          (_radiusPlanet + GROUND_OFFSET);
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, _planet.transform.position - offsetPosNormal);
                if (!Physics.CheckSphere(offsetPosNormal, 1.5f, _mask) && _pool.HasFreeElement(out Coin coin))
                {
                    coin.transform.position = offsetPosNormal;
                    coin.transform.rotation = rot;
                }
            }
        }
    }
}