using Codebase.Controllers;
using Codebase.Data;
using Codebase.ECS.Code.EcsWorld;
using Codebase.Pools;
using ECS.Code.Cars;
using ECS.Code.Inputs;
using ECS.Code.Planets;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Zenject;

namespace Codebase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private LazyInject<GameMachine> _gameMachine;
        private AudioService _audioService;
        private PostProcessVolume _postProcessVolume;
        private DataService _dataService;
        private WorldInitializer _worldInitializer;
        private PoolService _poolService;

        public BootstrapState(LazyInject<GameMachine> gameMachine, AudioService audioService,
            PostProcessVolume postProcessVolume, DataService dataService, WorldInitializer worldInitializer,
            PoolService poolService)
        {
            _gameMachine = gameMachine;
            _audioService = audioService;
            _postProcessVolume = postProcessVolume;
            _dataService = dataService;
            _worldInitializer = worldInitializer;
            _poolService = poolService;
        }

        public void Enter()
        {
            SetSettings();

            _poolService.CreatePools();
            _worldInitializer.CreateWorld();
            _gameMachine.Value.Enter<MenuState>();
        }

        private void SetSettings()
        {
            Application.targetFrameRate = 60;

            _audioService.SetMusicMute(!_dataService.Data.SettingsData.MusicEnable);
            _audioService.SetSoundMute(!_dataService.Data.SettingsData.SoundEnable);
            _postProcessVolume.gameObject.SetActive(_dataService.Data.SettingsData.GraphicQuality);
        }

        public void Exit()
        {
        }
    }
}