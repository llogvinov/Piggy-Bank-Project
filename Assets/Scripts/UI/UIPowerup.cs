using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIPowerup : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Image _powerupIcon;
        
        private void Start()
        {
            HidePanel();
            PowerUp.PowerupCollected += OnPowerUpCollected;
            PowerUp.PowerupEnded += OnPowerUpEnded;
        }

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