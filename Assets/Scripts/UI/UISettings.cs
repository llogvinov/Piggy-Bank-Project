using Core;
using Core.Services.PlayerData;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UISettings : MonoBehaviour
    {
        [SerializeField] private Button _musicButton;
        [SerializeField] private Button _noMusicButton;
        [Space]
        [SerializeField] private Button _soundButton;
        [SerializeField] private Button _noSoundButton;
        [Space]
        [SerializeField] private Button _noAddsButton;
        [Space]
        [SerializeField] private AudioSource _musicAudioSource;

        private IPlayerDataService _playerDataService;

        private void Awake()
        {
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();

            var musicOn = _playerDataService.GetMusic();
            _musicButton.gameObject.SetActive(musicOn);
            _noMusicButton.gameObject.SetActive(!musicOn);
            _musicAudioSource.volume = musicOn == true ? 1f : 0f;

            var soundOn = _playerDataService.GetSound();
            _soundButton.gameObject.SetActive(soundOn);
            _noSoundButton.gameObject.SetActive(!soundOn);

            RemoveAdsComplete();
        }

        private void Start()
        {
            _musicButton.onClick.AddListener(ToggleMusic);
            _noMusicButton.onClick.AddListener(ToggleMusic);

            _soundButton.onClick.AddListener(ToggleSound);
            _noSoundButton.onClick.AddListener(ToggleSound);
        }

        private void OnDestroy()
        {
            _musicButton.onClick.RemoveListener(ToggleMusic);
            _noMusicButton.onClick.RemoveListener(ToggleMusic);

            _soundButton.onClick.RemoveListener(ToggleSound);
            _noSoundButton.onClick.RemoveListener(ToggleSound);
        }

        private void ToggleMusic()
        {
            _playerDataService.SetMusic(!_playerDataService.GetMusic());
            var musicOn = _playerDataService.GetMusic();

            _musicButton.gameObject.SetActive(musicOn);
            _noMusicButton.gameObject.SetActive(!musicOn);

            _musicAudioSource.volume = musicOn == true ? 1f : 0f;
        }

        private void ToggleSound()
        {
            _playerDataService.SetSound(!_playerDataService.GetSound());
            var soundOn = _playerDataService.GetSound();

            _soundButton.gameObject.SetActive(soundOn);
            _noSoundButton.gameObject.SetActive(!soundOn);
        }

        public void RemoveAdsComplete()
        {
            if (_playerDataService.IsRemovedAds())
                _noAddsButton.gameObject.SetActive(false);
        }
    }
}