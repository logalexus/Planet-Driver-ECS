using Codebase.Content.Maps;
using Codebase.Controllers;
using Codebase.Infrastructure;
using Codebase.Infrastructure.States;
using Codebase.ZyphUI;
using Codebase.ZyphUI.Base;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Codebase.UI.Screens
{
    public class MainMenuScreen : BaseScreen
    {
        [Header("Buttons")] [SerializeField] private Button _start;
        [SerializeField] private Button _planets;
        [SerializeField] private Button _cars;
        [SerializeField] private Button _store;
        [SerializeField] private Button _settings;

        [Header("Fields")] 
        [SerializeField] private TextMeshProUGUI _coinCounter;
        [SerializeField] private TextMeshProUGUI _levelCounter;
        [SerializeField] private TextMeshProUGUI _mapName;

        [Header("Bars")] [SerializeField] private Slider _levelBar;

        private UIService _uiService;
        private PlanetService _planetService;
        private GameMachine _gameMachine;
        private PlayerService _playerService;

        [Inject]
        public void Init(UIService uiService, PlanetService planetService, GameMachine gameMachine,
            PlayerService playerService)
        {
            _uiService = uiService;
            _planetService = planetService;
            _gameMachine = gameMachine;
            _playerService = playerService;
        }

        private void Start()
        {
            _start.onClick.AddListener(() =>
            {
                _gameMachine.Enter<InGameState>();
                _uiService.OpenInGameScreen();
            });

            _planets.onClick.AddListener(_uiService.OpenPlanetsScreen);
            _cars.onClick.AddListener(_uiService.OpenCarsScreen);
            _store.onClick.AddListener(_uiService.OpenStoreScreen);
            _settings.onClick.AddListener(_uiService.OpenSettingsScreen);

            _playerService.CoinsChanged += () => { _coinCounter.text = $"{_playerService.Coins}$"; };
            _playerService.LevelChanged += () => { _levelCounter.text = $"lvl {_playerService.Level}"; };
            _playerService.ExpChanged += () => { _levelBar.value = _playerService.Exp; };
            _planetService.OnPlanetChanged += (name) => { _mapName.text = name; };
        }
    }
}