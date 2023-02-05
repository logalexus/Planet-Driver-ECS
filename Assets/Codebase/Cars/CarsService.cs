using Codebase.Assets;
using Codebase.Data;
using Codebase.EmtyComponents;
using Data.Models;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Codebase.Content.Cars
{
    public class CarsService : MonoBehaviour
    {
        [SerializeField] private Transform _carContainer;
        
        private DataService _dataService;
        private Car _currentCar;
        private CarsHolder _carsHolder;
        
        public Car CurrentCar => _currentCar;
        
        public UnityAction<BoxCollider> CarChanged;

        [Inject]
        public void Init(DataService dataService, AssetsProvider assetsProvider)
        {
            _dataService = dataService;
            _carsHolder = assetsProvider.CarsHolder;
        }

        private void Start()
        {
            CarModel carModel = _carsHolder.GetContent(_dataService.Data.CurrentCar);

            carModel ??= _carsHolder.Contents[0];

            FirstInit();
            SetContent(carModel);
        }

        private void FirstInit()
        {
            if (_dataService.Data.AvailableCarsData.Count == 0)
                _dataService.Data.AvailableCarsData.Add(_carsHolder.Contents[0].Name);
        }

        public void SetContent(CarModel carModel)
        {
            _dataService.Data.CurrentCar = carModel.Name;
        
            if (_carContainer.childCount != 0)
                Destroy(_carContainer.GetChild(0).gameObject);
        
            _currentCar = Instantiate(carModel.Prefab, _carContainer);
        
            CarChanged?.Invoke(_currentCar.GetComponent<BoxCollider>());
        }
    }
}
