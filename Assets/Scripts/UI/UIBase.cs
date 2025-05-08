using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIBase : MonoBehaviour
    {
        public event Action Opened, Hided;

        [SerializeField] protected GameObject _panel;
        [Space]
        [SerializeField] protected Button _openButton;
        [SerializeField] protected Button _closeButton;
        [Space]
        [SerializeField] protected GameObject[] _toHide;

        protected virtual void Start()
        {
            if (_openButton != null) _openButton.onClick.AddListener(Show);
            if (_closeButton != null) _closeButton.onClick.AddListener(Hide);
        }

        protected virtual void OnDestroy()
        {
            if (_openButton != null) _openButton.onClick.RemoveListener(Show);
            if (_closeButton != null) _closeButton.onClick.RemoveListener(Hide);
        }

        public virtual void Show()
        {
            _panel.SetActive(true);
            ToggleButtons(false);
            Opened?.Invoke();
        }

        public virtual void Hide()
        {
            _panel.SetActive(false);
            ToggleButtons(true);
            Hided?.Invoke();
        }

        protected void ToggleButtons(bool enable)
        {
            if (_toHide == null || _toHide.Length == 0) return;

            foreach (var toHide in _toHide)
                toHide.gameObject.SetActive(enable);
        }
    }
}