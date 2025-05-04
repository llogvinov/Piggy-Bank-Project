using Main;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIHealth : MonoBehaviour
    {
        [SerializeField] private Image[] _hearts;
        [SerializeField] private Sprite _fullHeart;
        [SerializeField] private Sprite _emptyHeart;

        private PlayerHealth _playerHealth;

        public void Initialize(Player player)
        {
            _playerHealth = player.Health;
            _playerHealth.HealthChanged += UpdateHeartsUI;
            UpdateHeartsUI(_playerHealth);
        }

        private void OnDestroy()
        {
            if (_playerHealth != null)
            {
                _playerHealth.HealthChanged -= UpdateHeartsUI;
            }
        }

        private void UpdateHeartsUI(PlayerHealth playerHealth)
        {
            for (int i = 0; i < _hearts.Length; i++)
            {
                _hearts[i].sprite = i < playerHealth.Health ? _fullHeart : _emptyHeart;
                _hearts[i].enabled = i < PlayerHealth.MAX_HEALTH;
            }
        }
    }
}