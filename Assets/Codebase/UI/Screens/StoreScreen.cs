using Codebase.Controllers;
using Codebase.ZyphUI;
using Codebase.ZyphUI.Base;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Codebase.UI.Screens
{
    public class StoreScreen : BaseScreen
    {
        [Header("Buttons")] 
        [SerializeField] private Button _back;
        
        [Header("Fields")] 
        [SerializeField] private TextMeshProUGUI _coinCounter;

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
            _back.onClick.AddListener(_uiService.BackToPreviousScreen);

            _playerService.CoinsChanged += () => { _coinCounter.text = $"{_playerService.Coins}$"; };
        }
    }
}