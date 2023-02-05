using System.Collections;
using Codebase.Content.Maps;
using Codebase.Controllers;
using Codebase.Data;
using Codebase.ZyphUI;
using Codebase.ZyphUI.Base;
using DG.Tweening;
using Lean.Gui;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Codebase.UI.Screens
{
    public class SettingsScreen : BaseScreen, IInitializable
    {
        [Header("Buttons")] 
        [SerializeField] private Button _back;

        [Header("Switchers")] 
        [SerializeField] private LeanToggle _musicSwitcher;
        [SerializeField] private LeanToggle _soundSwitcher;
        [SerializeField] private LeanToggle _postProcSwitcher;

        [Header("Graphic")] 
        [SerializeField] private GameObject _postProcessing;

        public bool MusicActive
        {
            get => _musicActive;
            set
            {
                _musicActive = value;
                _dataService.Data.SettingsData.MusicEnable = value;
                _dataService.Save();
                _audioService.SetMusicMute(!value);
            }
        }

        public bool SoundActive
        {
            get => _soundActive;
            set
            {
                _soundActive = value;
                _dataService.Data.SettingsData.SoundEnable = value;
                _dataService.Save();
                _audioService.SetSoundMute(!value);
            }
        }

        public bool PostProcActive
        {
            get => _postProcActive;
            set
            {
                _postProcActive = value;
                _dataService.Data.SettingsData.GraphicQuality = value;
                _dataService.Save();
                _postProcessing.SetActive(value);
            }
        }


        private bool _musicActive;
        private bool _soundActive;
        private bool _postProcActive;

        private DataService _dataService;
        private UIService _uiService;
        private PlanetService _planetService;
        private AudioService _audioService;

        [Inject]
        public void Init(UIService uiService, PlanetService planetService, DataService dataService,
            AudioService audioService)
        {
            _uiService = uiService;
            _planetService = planetService;
            _dataService = dataService;
            _audioService = audioService;
        }


        public void Start()
        {
            _back.onClick.AddListener(_uiService.BackToPreviousScreen);

            MusicActive = _dataService.Data.SettingsData.MusicEnable;
            SoundActive = _dataService.Data.SettingsData.SoundEnable;
            PostProcActive = _dataService.Data.SettingsData.GraphicQuality;
            
            _musicSwitcher.On = MusicActive;
            _soundSwitcher.On = SoundActive;
            _postProcSwitcher.On = PostProcActive;

            _musicSwitcher.OnOn.AddListener(() => MusicActive = true);
            _soundSwitcher.OnOn.AddListener(() => SoundActive = true);
            _postProcSwitcher.OnOn.AddListener(() => PostProcActive = true);
            
            _musicSwitcher.OnOff.AddListener(() => MusicActive = false);
            _soundSwitcher.OnOff.AddListener(() => SoundActive = false);
            _postProcSwitcher.OnOff.AddListener(() => PostProcActive = false);
        }
    }
}