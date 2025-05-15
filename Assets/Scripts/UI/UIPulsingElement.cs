using UnityEngine;

namespace UI
{
    public class UIPulsingElement : MonoBehaviour
    {
        [SerializeField] private float _pulseSpeed = 1.5f;
        [SerializeField] private float _scaleAmount = 1.1f;

        private Vector3 _originalScale = Vector3.one;

        private void OnEnable()
        {
            transform.localScale = _originalScale;
        }

        private void Update()
        {
            var scale = 1 + Mathf.Sin(Time.time * _pulseSpeed) * (_scaleAmount - 1);
            transform.localScale = _originalScale * scale;
        }
    }
}