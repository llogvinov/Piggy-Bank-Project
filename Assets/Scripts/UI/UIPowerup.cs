using Timer;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIPowerup : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Image _powerupIcon;
        [SerializeField] private GameTimer _timer;
        [SerializeField] private TimerUI _timerUI;

        private void Start()
        {
            HidePanel();

            _timer.TimerSet += OnTimerSet;
            _timer.TimerCompleted += OnTimerCompleted;

            PowerUp.PowerupCollected += OnPowerUpCollected;
            PowerUp.PowerupEnded += OnPowerUpEnded;
        }

        private void OnDestroy()
        {
            _timer.TimerSet -= OnTimerSet;
            _timer.TimerCompleted -= OnTimerCompleted;

            PowerUp.PowerupCollected -= OnPowerUpCollected;
            PowerUp.PowerupEnded -= OnPowerUpEnded;
        }

        private void OnTimerSet() =>
            _timerUI.gameObject.SetActive(true);

        private void OnTimerCompleted() =>
            _timerUI.gameObject.SetActive(false);

        private void OnPowerUpCollected(PowerupEventArgs args)
        {
            UpdatePanel(args);
            ShowPanel();
        }

        private void ShowPanel() =>
            _panel.SetActive(true);

        private void HidePanel() =>
            _panel.SetActive(false);

        private void UpdatePanel(PowerupEventArgs args) =>
            _powerupIcon.sprite = args.Sprite;

        private void OnPowerUpEnded() =>
            HidePanel();
    }
}