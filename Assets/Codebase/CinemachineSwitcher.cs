using Codebase.Content.Cars;
using Codebase.Controllers;
using UnityEngine;
using Zenject;

namespace Codebase
{
    public class CinemachineSwitcher : MonoBehaviour
    {
        [SerializeField] private Transform cameraPath;
        [SerializeField] private GameObject _aroundCamera;
        [SerializeField] private GameObject _playingCamera;

        private float _cameraPathOffset = 5f;
        private CarsService _carsService;
        
        [Inject]
        public void Init(CarsService carsService)
        {
            _carsService = carsService;
        }
        
        public void SwitchAroundCamera()
        {
            _aroundCamera.SetActive(true);
            _playingCamera.SetActive(false);
        }

        public void SwitchPlayingCamera()
        {
            _aroundCamera.SetActive(false);
            _playingCamera.SetActive(true);
            Transform currentCar = _carsService.CurrentCar.transform;
            cameraPath.position = currentCar.position + currentCar.up * _cameraPathOffset;
            cameraPath.rotation = currentCar.rotation;
        }
    }
}
