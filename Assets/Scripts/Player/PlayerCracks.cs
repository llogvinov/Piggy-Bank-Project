using Core;
using UnityEngine;

namespace Main
{
    public class PlayerCracks : MonoBehaviour
    {
        [SerializeField] private GameObject _smallCrack;
        [SerializeField] private GameObject _bigCrack;
        [SerializeField] private CrackedPlayer _crackedPlayerPrefab;

        private Player _player;

        public void Initialize(Player player)
        {
            _player = player;
            _player.Health.HealthChanged += UpdatePlayerVisual;
        }

        private void OnDestroy()
        {
            if (_player.Health != null)
            {
                _player.Health.HealthChanged -= UpdatePlayerVisual;
            }
        }

        private void UpdatePlayerVisual(PlayerHealth playerHealth)
        {
            switch (playerHealth.Health)
            {
                case 2:
                    {
                        _smallCrack.SetActive(true);
                        _bigCrack.SetActive(false);
                        break;
                    }
                case 1:
                    {
                        _smallCrack.SetActive(true);
                        _bigCrack.SetActive(true);
                        break;
                    }
                case 0:
                    {
                        _player.Health.gameObject.SetActive(false);
                        Instantiate(_crackedPlayerPrefab, playerHealth.transform.position, playerHealth.transform.rotation, transform);
                        Game.GameOver?.Invoke(GameOverCondition.Died);
                        break;
                    }
                default:
                    {
                        _smallCrack.SetActive(false);
                        _bigCrack.SetActive(false);
                        break;
                    }
            }
        }
    }
}