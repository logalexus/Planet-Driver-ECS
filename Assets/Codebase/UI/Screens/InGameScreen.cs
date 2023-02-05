using Codebase.Controllers;
using Codebase.ZyphUI;
using Codebase.ZyphUI.Base;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Codebase.UI.Screens
{
    public class InGameScreen : BaseScreen
    {
        [Header("Fields")]
        [SerializeField] private TextMeshProUGUI _coinCounter;
        [SerializeField] private TextMeshProUGUI _distanceCounter;

        private UIService _uiService;
        private PlayerService _playerService;
        
        [Inject]
        public void Init(UIService uiService, PlayerService playerService)
        {
            _uiService = uiService;
            _playerService = playerService;
        }

        private void Start()
        {
            _playerService.CollectedCoinsInGameChanged += ()=> 
            {
                _coinCounter.text = "x" + _playerService.CollectedCoinsInGame.ToString();
            };
            _playerService.DistanceChanged += () =>
            {
                _distanceCounter.text = _playerService.CompletedDistanceChanged.ToString() + "m";
            };
        }

    
    
    }
}
