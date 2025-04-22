using Core;
using Core.Services.PlayerData;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UISettings : UIBase
    {
        [SerializeField] private Button _musicButton;
        [SerializeField] private Button _soundButton;
        [SerializeField] private Button _noAddsButton;
        [Space]
        [SerializeField] private GameObject _noMusicImage;
        [SerializeField] private GameObject _noSoundImage;
        [Space]
        [SerializeField] private AudioSource _musicAudioSource;

        private IPlayerDataService _playerDataService;

        private void Awake()
        {
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
            
            var music = _playerDataService.GetMusic();
            _musicAudioSource.volume = music == true ? 1f : 0f;
            _noMusicImage.SetActive(!music);

            _noSoundImage.SetActive(!_playerDataService.GetSound());

            RemoveAdsComplete();
        }

        protected override void Start()
        {
            _openButton.onClick.AddListener(ToggleCanvas);
            _musicButton.onClick.AddListener(ToggleMusic);
            _soundButton.onClick.AddListener(ToggleSound);
        }

        protected override void OnDestroy()
        {
            _openButton.onClick.RemoveListener(ToggleCanvas);
            _musicButton.onClick.RemoveListener(ToggleMusic);
            _soundButton.onClick.RemoveListener(ToggleSound);
        }

        private void ToggleCanvas()
        {
            if (_panel.gameObject.activeSelf == false)
                Show();
            else
                Hide();
        }

        private void ToggleMusic()
        {
            _playerDataService.SetMusic(!_playerDataService.GetMusic());
            var newValue = _playerDataService.GetMusic();

            _noMusicImage.SetActive(!newValue);
            _musicAudioSource.volume = newValue == true ? 1f : 0f;
        }

        private void ToggleSound()
        {
            _playerDataService.SetSound(!_playerDataService.GetSound());
            var newValue = _playerDataService.GetSound();

            _noSoundImage.SetActive(!newValue);
        }

        public void RemoveAdsComplete()
        {
            if (_playerDataService.IsRemovedAds())
                _noAddsButton.gameObject.SetActive(false);
        }
    }
}