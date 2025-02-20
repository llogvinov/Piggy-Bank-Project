using UnityEngine;

namespace UI
{
    public class UILoading : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public void Show() => 
            _canvasGroup.alpha = 1f;

        public void Hide() => 
            _canvasGroup.alpha = 0f;
    }
}