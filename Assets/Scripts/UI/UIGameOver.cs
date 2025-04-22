using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIGameOver : MonoBehaviour
    {
        [SerializeField] protected Canvas _canvas;
        [Space]
        [SerializeField] private Button _restartButton;
        [SerializeField] protected Button _menuButton;
        [Space]
        [SerializeField] private Text _recordText;
        [SerializeField] private Text _coinToAddText;
        [SerializeField] private Text _totalCoinsText;

        public Button RestartButton => _restartButton;
        public Button MenuButton => _menuButton;

        public void Show()
        {
            _canvas.gameObject.SetActive(true);
        }

        public void Show(int record, int coinsToAdd, int totalCoins)
        {
            UpdateUI(record, coinsToAdd, totalCoins);
            Show();
        }

        public void Hide()
        {
            _canvas.gameObject.SetActive(false);
        }

        public void UpdateUI(int record, int coinsToAdd, int totalCoins)
        {
            _recordText.text = "record: " + record.ToString();
            _coinToAddText.text = "+" + coinsToAdd.ToString();
            _totalCoinsText.text = totalCoins.ToString();
        }
    }
}