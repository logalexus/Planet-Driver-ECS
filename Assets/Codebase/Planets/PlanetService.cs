using System;
using Codebase.Assets;
using Codebase.Data;
using ECS.Code.Planets;
using UnityEngine;
using Zenject;

namespace Codebase.Content.Maps
{
    public class PlanetService : MonoBehaviour
    {
        [SerializeField] private Transform _planetContainer;

        private PlanetProvider _currentPlanet;
        private DataService _dataService;
        private PlanetHolder _planetHolder;

        public PlanetProvider CurrentPlanet => _currentPlanet;

        public Action<string> OnPlanetChanged;

        [Inject]
        public void Init(DataService dataService, AssetsProvider assetsProvider)
        {
            _dataService = dataService;
            _planetHolder = assetsProvider.PlanetHolder;
        }

        private void Start()
        {
            PlanetModel planetModel = _planetHolder.GetContent(_dataService.Data.CurrentPlanet);

            planetModel ??= _planetHolder.Contents[0];

            FirstInit();
            SetPlanet(planetModel);
        }

        private void FirstInit()
        {
            if (_dataService.Data.AvailablePlanetsData.Count == 0)
                _dataService.Data.AvailablePlanetsData.Add(_planetHolder.Contents[0].Name);
        }
    
        public void SetPlanet(PlanetModel planetData)
        {
            _dataService.Data.CurrentPlanet = planetData.Name;
        
            if (_planetContainer.childCount != 0)
                Destroy(_planetContainer.GetChild(0).gameObject);
        
            _currentPlanet = Instantiate(planetData.Prefab, _planetContainer);
            RenderSettings.skybox = planetData.SkyBox;
        
            OnPlanetChanged?.Invoke(planetData.Name);
        }
    
    }
}
