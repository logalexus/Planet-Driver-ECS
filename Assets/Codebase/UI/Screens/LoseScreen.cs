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
    public class LoseScreen : BaseScreen
    {
        [SerializeField] private CinemachineSwitcher _cinemachineSwitcher;
        [SerializeField] private GameObject _X2Element;
        [Header("Buttons")]
        [SerializeField] private Button _collect;
        [SerializeField] private Button _again;
        [SerializeField] private Button _continue;
        [SerializeField] private Button _rewardX2;
        [Header("Fields")]
        [SerializeField] private TextMeshProUGUI _coinCounter;
        [SerializeField] private TextMeshProUGUI _distanceCounter;
        [SerializeField] private RectTransform _resultGroup;

        private float _durationScroll = 0.3f;
        private float _overshot = 1f;

        private UIService _uiService;
        private PlayerService _playerService;
        private GameMachine _gameMachine;
        
        [Inject]
        public void Init(UIService uiService, PlayerService playerService, GameMachine gameMachine)
        {
            _uiService = uiService;
            _playerService = playerService;
            _gameMachine = gameMachine;
        }
    
        private void Start()
        {
            _collect.onClick.AddListener(()=> 
            {
                ScrollResults();
                _playerService.Coins += _playerService.CollectedCoinsInGame;
                _playerService.AddExp(_playerService.CompletedDistanceChanged);
            });

            _again.onClick.AddListener(_gameMachine.Enter<InGameState>);
            _continue.onClick.AddListener(_gameMachine.Enter<MenuState>);

            _rewardX2.onClick.AddListener(() =>
            {
                _X2Element.SetActive(false);
            });

            _playerService.CollectedCoinsInGameChanged += () =>
            {
                _coinCounter.text = $"{_playerService.CollectedCoinsInGame}$";
            };
        }
    
        public override void Open()
        {
            base.Open();
            _X2Element.SetActive(true);
            _coinCounter.text = $"{_playerService.CollectedCoinsInGame}$";
            _distanceCounter.text = $"{_playerService.CompletedDistanceChanged}m";
        }

        public void ScrollResults()
        {
            _resultGroup.DOLocalMoveX(_resultGroup.localPosition.x - _resultGroup.rect.width / 2, _durationScroll)
                .SetEase(Ease.OutBack, _overshot);
        }

    }
}
