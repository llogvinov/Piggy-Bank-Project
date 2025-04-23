using Core;
using Core.Services.PlayerData;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CoinsPanel : MonoBehaviour
    {
        [SerializeField] private Text _coinsText;

        private IPlayerDataService _playerDataService;

        private void Awake()
        {
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
        }

        private void OnEnable()
        {
            SetCoinsText(_playerDataService.GetCoins());
        }

        private void SetCoinsText(int value) => _coinsText.text = value.ToString();
    }
}