using Core;
using Core.Services.PlayerData;
using UnityEngine;

namespace Main
{
    public class PlayerSkinCreator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _hat;
        [SerializeField] private SpriteRenderer _mask;

        private IPlayerDataService _playerDataService;

        private void Awake()
        {
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
        }

        private void Start()
        {
            SetFullSkin();
        }

        public void SetFullSkin()
        {
            SetMask();
            SetHat();
        }

        public void SetMask()
        {
            var mask = _playerDataService.GetSelectedMask();
            _mask.sprite = mask.Image;
        }

        public void SetHat()
        {
            var hat = _playerDataService.GetSelectedHat();
            _hat.sprite = hat.Image;
        }
    }
}