using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIBase : MonoBehaviour
    {
        [SerializeField] protected Canvas Canvas;
        [Space]
        [SerializeField] protected Button _openButton;
        [SerializeField] protected Button _closeButton;
        [Space]
        [SerializeField] protected GameObject[] _toHide;

        protected virtual void Start()
        {
            _openButton.onClick.AddListener(Show);
            _closeButton.onClick.AddListener(Hide);
        }

        protected virtual void OnDestroy()
        {
            _openButton.onClick.RemoveListener(Show);
            _closeButton.onClick.RemoveListener(Hide);
        }

        public virtual void Show()
        {
            Canvas.gameObject.SetActive(true);
            ToggleButtons(false);
        }

        public virtual void Hide()
        {
            Canvas.gameObject.SetActive(false);
            ToggleButtons(true);
        }

        protected void ToggleButtons(bool enable)
        {
            if (_toHide == null || _toHide.Length == 0) return;

            foreach (var toHide in _toHide)
                toHide.gameObject.SetActive(enable);
        }
    }
}