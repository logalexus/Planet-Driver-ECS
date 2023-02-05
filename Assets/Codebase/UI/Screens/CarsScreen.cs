using System.Collections;
using Codebase.Assets;
using Codebase.Content.Cars;
using Codebase.Controllers;
using Codebase.Data;
using Codebase.ZyphUI;
using Codebase.ZyphUI.Base;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using Zenject;

namespace Codebase.UI.Screens
{
    public class CarsScreen : BaseScreen
    {
        [SerializeField] private HorizontalScrollSnap _scroller;
        [SerializeField] private Button _back;
        [SerializeField] private TextMeshProUGUI _coinCounter;
        [SerializeField] private TextMeshProUGUI _levelCounter;

        private UIService _uiService;
        private PlayerService _playerService;
        private DataService _dataService;
        private DiContainer _diContainer;
        private CarsHolder _carsHolder;
        private AssetsProvider _assetsProvider;

        [Inject]
        public void Init(UIService uiService, PlayerService playerService, DataService dataService,
            DiContainer diContainer, AssetsProvider assetsProvider)
        {
            _uiService = uiService;
            _playerService = playerService;
            _dataService = dataService;
            _diContainer = diContainer;
            _assetsProvider = assetsProvider;
            _carsHolder = _assetsProvider.CarsHolder;
        }

        private void Start()
        {
            _back.onClick.AddListener(() => { _uiService.BackToPreviousScreen(); });
            _playerService.CoinsChanged += () => { _coinCounter.text = $"{_playerService.Coins}$"; };
            _playerService.LevelChanged += () => { _levelCounter.text = $"lvl {_playerService.Level}"; };

            SetCarsUI();
        }


        private void SetCarsUI()
        {
            for (int i = 0; i < _carsHolder.Contents.Count; i++)
            {
                CarUI carUI = _diContainer.InstantiatePrefab(_assetsProvider.CarUI).GetComponent<CarUI>();
                carUI.Init(_carsHolder.Contents[i]);
                _scroller.AddChild(carUI.gameObject);
            }
        }
    }
}