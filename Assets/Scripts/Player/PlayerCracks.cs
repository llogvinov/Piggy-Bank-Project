using Core;
using Core.Factory;
using UnityEngine;

namespace Main.Player
{
    public class PlayerCracks : MonoBehaviour
    {
        [SerializeField] private GameObject _smallCrack;
        [SerializeField] private GameObject _bigCrack;
        [SerializeField] private CrackedPlayer _crackedPlayerPrefab;

        private PlayerHealth _playerHealth;

        private void Awake()
        {
            _playerHealth = AllServices.Container.Single<IGameFactory>()
                .Player.GetComponent<PlayerHealth>();
        }

        private void Start()
        {
            if (_playerHealth != null)
            {
                _playerHealth.HealthChanged += UpdatePlayerVisual;
            }
        }

        private void OnDestroy()
        {
            if (_playerHealth != null)
            {
                _playerHealth.HealthChanged -= UpdatePlayerVisual;
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
                    Instantiate(_crackedPlayerPrefab, playerHealth.transform.position, playerHealth.transform.rotation);
                    Game.GameOver?.Invoke();
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