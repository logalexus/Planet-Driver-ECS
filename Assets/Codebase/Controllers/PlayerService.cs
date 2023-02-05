using System;
using System.Collections;
using Codebase.Content.Cars;
using Codebase.Data;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Codebase.Controllers
{
    public class PlayerService : MonoBehaviour
    {
        [SerializeField] private Transform _mainPosition;
        [SerializeField] private BoxCollider _collider;

        public Vector3 ContactPosition { get; private set; }
        
        public int CollectedCoinsInGame
        {
            get => _collectedCoinsInGame;
            set
            {
                _collectedCoinsInGame = value;
                CollectedCoinsInGameChanged?.Invoke();
            }
        }
        public int Coins
        {
            get => _coins;
            set
            {
                _coins = value;
                _dataService.Data.ProgressData.Money = value;
                _dataService.Save();
                CoinsChanged?.Invoke();
            }
        }
        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                _dataService.Data.ProgressData.Level = value;
                _dataService.Save();
                LevelChanged?.Invoke();
            }
        }
        public int Exp
        {
            get => _exp;
            set
            {
                _exp = value;
                _dataService.Data.ProgressData.Exp = value;
                _dataService.Save();
                ExpChanged?.Invoke();
            }
        }
        public int CompletedDistanceChanged
        {
            get => _completedDistanceChanged;
            set
            {
                _completedDistanceChanged = value;
                DistanceChanged?.Invoke();
            }
        }

        public Action CarCrush;
        public Action CollectedCoinsInGameChanged;
        public Action CoinsChanged;
        public Action LevelChanged;
        public Action ExpChanged;
        public Action DistanceChanged;
    
        private bool _isCollisionAlready = false;
        private int _collectedCoinsInGame;
        private int _coins;
        private int _level;
        private int _exp;
        private int _completedDistanceChanged;
        private int _maxExp = 1000;
        
        private Vector3 _oldPos;
        private DataService _dataService;
        private AudioService _audioService;
        private CarsService _carsService;
    

        [Inject]
        public void Init(DataService dataService, CarsService carsService, AudioService audioService)
        {
            _dataService = dataService;
            _carsService = carsService;
            _audioService = audioService;
        }
        
        private void Start()
        {
            _carsService.CarChanged += ChangeCollider;
            _oldPos = transform.position;
            
            StartCoroutine(CountMetres());
            StartCoroutine(InitStatsUI());
        }

        private void ChangeCollider(BoxCollider newCollider)
        {
            _collider.size = newCollider.size;
            _collider.center = newCollider.center;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Crush") && !_isCollisionAlready)
            {
                _audioService.PlaySFX(_audioService.Sounds.Collision);
                ContactPosition = collision.contacts[0].point;
                CarCrush?.Invoke();
                StopCoroutine(CountMetres());
                _isCollisionAlready = true;
            }
        }
    
        public void RestartPlayer()
        {
            transform.position = _mainPosition.position;
            transform.rotation = _mainPosition.rotation;
            _isCollisionAlready = false;
            CollectedCoinsInGame = 0;
            CompletedDistanceChanged = 0;
        }

        private IEnumerator InitStatsUI()
        {
            yield return null;
            Coins = _dataService.Data.ProgressData.Money;
            Level = _dataService.Data.ProgressData.Level;
            Exp = _dataService.Data.ProgressData.Exp;
        }

        private IEnumerator CountMetres()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);

                float deltaDist = (transform.position - _oldPos).magnitude;
                _oldPos = transform.position;
                CompletedDistanceChanged += (int)deltaDist;
            }
        }

        public void CoinCollecting()
        {
            CollectedCoinsInGame += 10;
            _audioService.PlaySFX(_audioService.Sounds.Coin);
        }

        public void AddExp(int value)
        {
            Exp += value;
            if (Exp >= _maxExp)
            {
                Level++;
                Exp -= _maxExp;
            }
        }
    }
}
