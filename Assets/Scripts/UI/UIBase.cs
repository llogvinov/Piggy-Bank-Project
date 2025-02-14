using UnityEngine;

namespace UI
{
    public class UIBase : MonoBehaviour
    {
        public Canvas Canvas;

        [SerializeField] protected GameObject[] _toHide;

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