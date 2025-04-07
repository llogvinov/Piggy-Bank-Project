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
        [SerializeField] private Image noMusicImage;
        [SerializeField] private Image noSoundImage;
        
        private IPlayerDataService _playerDataService;
        
        private void Awake()
        {
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
        }


        protected override void Start()
        {
            _openButton.onClick.AddListener(ToggleCanvas);
            _musicButton.onClick.AddListener(ToggleMusic);
            _soundButton.onClick.AddListener(ToggleSound);

            RemoveAdsComplete();

            if (PlayerPrefs.GetFloat("music") == 0)
                noMusicImage.gameObject.SetActive(true);
            if (PlayerPrefs.GetFloat("sounds") == 0)
                noSoundImage.gameObject.SetActive(true);
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
            var noMusic = noMusicImage.gameObject;
            noMusic.SetActive(!noMusic.activeSelf);

            PlayerPrefs.SetFloat("music", noMusic.activeSelf ? 0f : 1f);
        }

        private void ToggleSound()
        {
            var noSound = noSoundImage.gameObject;
            noSound.SetActive(!noSound.activeSelf);

            PlayerPrefs.SetFloat("sounds", noSound.activeSelf ? 0f : 1f);
        }

        public void RemoveAdsComplete()
        {
            if (_playerDataService.IsRemovedAds())
                _noAddsButton.gameObject.SetActive(false);
        }
    }
}