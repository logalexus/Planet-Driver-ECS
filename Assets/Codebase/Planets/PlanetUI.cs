using Codebase.Content.Maps;
using Codebase.Controllers;
using Codebase.Data;
using TMPro;
using UI.Popups;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Codebase.UI
{
    public class PlanetUI : MonoBehaviour
    {
        [SerializeField] private GameObject _accessPanel;
        [SerializeField] private GameObject _lockLabel;
        [SerializeField] private GameObject _costLabel;
        [SerializeField] private Image _mapPreview;
        [SerializeField] private WarningAnimation _mapWarning;

        [Header("Fields")] [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _targetLevel;
        [SerializeField] private TextMeshProUGUI _cost;
        [SerializeField] private TextMeshProUGUI _record;

        [Header("Buttons")] [SerializeField] private Button _selectButton;

        private UIService _uiService;
        private DataService _dataService;
        private PopupService _popupService;
        private PlanetService _planetService;
        private PlayerService _playerService;
        private PlanetModel _planetModel;
        private bool _access;

        [Inject]
        public void Init(UIService uiService, DataService dataService, PopupService popupService,
            PlanetService planetService, PlayerService playerService)
        {
            _uiService = uiService;
            _dataService = dataService;
            _popupService = popupService;
            _planetService = planetService;
            _playerService = playerService;
        }

        public bool AccessMap
        {
            get => _access;
            set
            {
                _access = value;
                _accessPanel.SetActive(value);
                _lockLabel.SetActive(!value);
            }
        }

        public void Init(PlanetModel planetModel)
        {
            _planetModel = planetModel;

            AccessMap = _dataService.Data.AvailablePlanetsData.Contains(_planetModel.Name);

            _mapPreview.sprite = _planetModel.Preview;
            _name.text = _planetModel.Name;
            _cost.text = _planetModel.Cost.ToString();
            _targetLevel.text = $"{_planetModel.TargetLevel} lvl";
            //_record.text = $"{_planetModel.Record}m";
            _playerService.LevelChanged += CheckLevel;
            
            CheckLevel();

            _selectButton.onClick.AddListener(OnClickMap);
        }


        private void OnClickMap()
        {
            if (AccessMap)
            {
                ShowMap();
            }
            else
            {
                if (_playerService.Coins >= _planetModel.Cost && _playerService.Level >= _planetModel.TargetLevel)
                {
                    _popupService.ShowApprovePopup("Are you sure?", "Warning", () => Buy());
                }
                else
                {
                    _selectButton.interactable = false;
                    _mapWarning.StartAnimation(() => _selectButton.interactable = true);
                }
            }
        }

        private void Buy()
        {
            _playerService.Coins -= _planetModel.Cost;
            AccessMap = true;

            _dataService.Save();
            _dataService.Data.AvailablePlanetsData.Add(_planetModel.Name);
            _popupService.ClosePopup();
        }

        private void CheckLevel()
        {
            bool value = _playerService.Level >= _planetModel.TargetLevel;
            _costLabel.SetActive(value);
            _targetLevel.gameObject.SetActive(!value);
        }

        private void ShowMap()
        {
            _planetService.SetPlanet(_planetModel);
            _uiService.OpenMainMenuScreen();
            _dataService.Save();
        }
    }
}