using System;
using Core;
using Core.Services.PlayerData;
using UnityEngine;

namespace Main
{
    public class PlayerCoinCollector : MonoBehaviour
    {
        public event Action<int> CoinsUpdated;

        [SerializeField] private AudioClip coinClip;

        private AudioSource playerAudio;

        private int _coinsToAdd;
        public int CoinsToAdd
        {
            get => _coinsToAdd;
            private set
            {
                _coinsToAdd = value;
                CoinsUpdated?.Invoke(value);
            }
        }

        private IPlayerDataService _playerDataService;

        private void Awake()
        {
            playerAudio = GetComponent<AudioSource>();

            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
            playerAudio.volume = _playerDataService.GetSound() == true ? 1f : 0f;
        }

        private void Start()
        {
            CoinsToAdd = 0;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.TryGetComponent(out Coin coin);

            if (coin != null)
                CollectCoin(coin);
        }

        private void CollectCoin(Coin coin)
        {
            var multiplier = PowerUp.IsDoubleCoinsActive == false ? 1 : 2;
            CoinsToAdd += multiplier * coin.CoinValue;
            playerAudio.PlayOneShot(coinClip, 1);
            Destroy(coin.gameObject);
        }
    }
}