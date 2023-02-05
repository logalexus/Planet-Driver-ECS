using System.Collections;
using Codebase.Assets;
using Codebase.Content.Maps;
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
    public class PlanetsScreen : BaseScreen
    {
        [SerializeField] private Transform uiPlanetContainer;
        [SerializeField] private Button _back;
        [SerializeField] private TextMeshProUGUI _coinCounter;
        [SerializeField] private TextMeshProUGUI _levelCounter;

        private UIService _uiService;
        private DataService _dataService;
        private PlayerService _playerService;
        private DiContainer _diContainer;
        private PlanetHolder _planetHolder;
        private AssetsProvider _assetsProvider;


        [Inject]
        public void Init(UIService uiService, DataService dataService, PlayerService playerService,
            DiContainer diContainer, AssetsProvider assetsProvider)
        {
            _uiService = uiService;
            _dataService = dataService;
            _playerService = playerService;
            _diContainer = diContainer;
            _assetsProvider = assetsProvider;
            _planetHolder = _assetsProvider.PlanetHolder;
        }

        private void Start()
        {
            _back.onClick.AddListener(_uiService.BackToPreviousScreen);

            _playerService.CoinsChanged += () => { _coinCounter.text = $"{_playerService.Coins}$"; };
            _playerService.LevelChanged += () => { _levelCounter.text = $"lvl {_playerService.Level}"; };

            SetMapsUI();
        }


        private void SetMapsUI()
        {
            for (int j = 0; j < _planetHolder.Contents.Count; j++)
            {
                PlanetUI planetUI = _diContainer.InstantiatePrefab(_assetsProvider.PlanetUI, uiPlanetContainer).GetComponent<PlanetUI>();
                planetUI.Init(_planetHolder.Contents[j]);
            }
        }
    }
}