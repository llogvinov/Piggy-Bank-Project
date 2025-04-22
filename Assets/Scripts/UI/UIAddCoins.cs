using Main;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIAddCoins : MonoBehaviour
    {
        [SerializeField] private Text _coinsToAdd;

        private Player _player;

        public void Initialize(Player player)
        {
            _player = player;
            _player.CoinCollector.CoinsUpdated += OnCoinsUpdated;
        }

        private void OnDestroy()
        {
            if (_player != null)
                _player.CoinCollector.CoinsUpdated -= OnCoinsUpdated;
        }

        private void OnCoinsUpdated(int newValue)
        {
            _coinsToAdd.text = "+" + newValue.ToString();
        }
    }
}