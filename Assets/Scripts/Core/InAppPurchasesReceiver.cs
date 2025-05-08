using Core.Services.PlayerData;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Core
{
    public class InAppPurchasesReceiver : MonoBehaviour
    {
        public UnityEvent SuccessPurchased, FailedPurchased;

        private IPlayerDataService _playerDataService;

        private void Awake()
        {
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
        }

        private void OnEnable()
        {
            YG2.onPurchaseSuccess += OnSuccessPurchased;
            YG2.onPurchaseFailed += OnFailedPurchased;
        }

        private void OnDisable()
        {
            YG2.onPurchaseSuccess -= OnSuccessPurchased;
            YG2.onPurchaseFailed -= OnFailedPurchased;
        }

        private void OnSuccessPurchased(string id)
        {
            SuccessPurchased?.Invoke();

            switch (id)
            {
                case "1":
                    _playerDataService.AddCoins(1000);
                    break;
                case "2":
                    _playerDataService.AddCoins(3000);
                    break;
                case "3":
                    _playerDataService.AddCoins(10000);
                    break;
                case "4":
                    _playerDataService.AddCoins(40000);
                    break;
                default:
                    Debug.LogError("purchase id is invalid");
                    break;
            }
        }

        private void OnFailedPurchased(string id)
        {
            FailedPurchased?.Invoke();
        }
    }
}