using System.Collections;
using Core;
using Core.Factory;
using Data;
using Main;
using Timer;
using UnityEngine;

namespace Spawners
{
    public class PowerupSpawner : PooledObjectSpawner
    {
        private IGameFactory _gameFactory;
        private GameTimer _timer;

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _timer = GameObject.FindObjectOfType<GameTimer>();
        }

        private void Start()
        {
            PowerUp.PowerupCollected += OnPowerupCollected;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            PowerUp.PowerupCollected -= OnPowerupCollected;
        }

        private void SetTimer(float duration)
        {
            _timer.SetTimer(duration);
        }

        public void OnPowerupCollected(PowerupEventArgs args)
        {
            switch (args.index)
            {
                case 0:
                    StartCoroutine(DoubleCoinsPowerup(GameConstants.DOUBLE_COINS_POWERUP_TIMER));
                    break;
                case 1:
                    StartCoroutine(ShieldPowerUp(GameConstants.SHIELD_POWERUP_TIMER));
                    break;
                case 2:
                    StartCoroutine(SuperSpeedPowerUp(GameConstants.SPEED_POWERUP_TIMER));
                    break;
                case 3:
                    StartCoroutine(HeartPowerUp());
                    break;
            }
        }

        private IEnumerator DoubleCoinsPowerup(float duration)
        {
            SetTimer(duration);
            PowerUp.IsDoubleCoinsActive = true;

            yield return new WaitForSeconds(duration);

            PowerUp.IsDoubleCoinsActive = false;
            PowerUp.PowerupEnded?.Invoke();
        }

        private IEnumerator ShieldPowerUp(float duration)
        {
            SetTimer(duration);
            PowerUp.IsShieldActive = true;

            yield return new WaitForSeconds(duration);

            PowerUp.IsShieldActive = false;
            PowerUp.PowerupEnded?.Invoke();
        }

        private IEnumerator SuperSpeedPowerUp(float duration)
        {
            SetTimer(duration);
            PowerUp.IsSuperSpeedActive = true;

            yield return new WaitForSeconds(duration);

            PowerUp.IsSuperSpeedActive = false;
            PowerUp.PowerupEnded?.Invoke();
        }

        private IEnumerator HeartPowerUp()
        {
            _gameFactory.Player.Health.AddHeart();

            yield return new WaitForSeconds(2);

            PowerUp.PowerupEnded?.Invoke();
        }
    }
}