using Codebase.Content.Cars;
using Codebase.Controllers;
using Codebase.Data;
using Codebase.UI.Screens;
using Data.Models;
using TMPro;
using UI.Popups;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using Zenject;

namespace Codebase.UI
{
    public class CarUI : MonoBehaviour
    {
        [SerializeField] private GameObject _accessPanel;
        [SerializeField] private Image _carPreview;
        [SerializeField] private WarningAnimation _mapWarning;

        [Header("Fields")] [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _cost;

        [Header("Buttons")] [SerializeField] private Button _selectButton;

        private CarModel _carModel;
        private bool _access;
        private PopupService _popupService;
        private CarsService _carsService;
        private PlayerService _playerService;
        private DataService _dataService;
        private UIService _uiService;

        [Inject]
        public void Init(PopupService popupService, CarsService carsService, PlayerService playerService,
            DataService dataService, UIService uiService)
        {
            _popupService = popupService;
            _carsService = carsService;
            _playerService = playerService;
            _dataService = dataService;
            _uiService = uiService;
        }


        public bool AccessCar
        {
            get => _access;
            set
            {
                _access = value;
                _accessPanel.SetActive(value);
            }
        }

        public void Init(CarModel autoData)
        {
            _carModel = autoData;

            AccessCar = _dataService.Data.AvailableCarsData.Contains(_carModel.Name);

            _name.text = _carModel.Name;
            _cost.text = _carModel.Cost.ToString();
            _carPreview.sprite = _carModel.Preview;

            _selectButton.onClick.AddListener(OnClickCar);
        }

        private void OnClickCar()
        {
            if (AccessCar)
                ShowCar();
            else
            {
                if (_playerService.Coins >= _carModel.Cost)
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
            AccessCar = true;
            
            _playerService.Coins -= _carModel.Cost;
            _dataService.Save();
            _dataService.Data.AvailableCarsData.Add(_carModel.Name);
        }

        private void ShowCar()
        {
            _carsService.SetContent(_carModel);
            _uiService.OpenMainMenuScreen();
            _dataService.Save();
        }


    }
}