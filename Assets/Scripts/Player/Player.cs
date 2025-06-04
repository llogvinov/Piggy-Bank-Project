using UnityEngine;

namespace Main
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerCoinCollector _playerCoinCollector;
        [SerializeField] private PlayerSkinCreator _playerSkinCreator;
        [SerializeField] private PlayerCracks _playerCracks;

        public PlayerInput Input => _playerInput;
        public PlayerMovement Movement => _playerMovement;
        public PlayerHealth Health => _playerHealth;
        public PlayerCoinCollector CoinCollector => _playerCoinCollector;
        public PlayerSkinCreator SkinCreator => _playerSkinCreator;
        public PlayerCracks Cracks => _playerCracks;

        private void Awake()
        {
            Health.SetInitialHealth();
            Cracks.Initialize(this);
        }

        public void ToggleMovement(bool enable) =>
            _playerMovement.enabled = enable;

        public void ToggleHealth(bool enable) =>
            _playerHealth.enabled = enable;
    }
}