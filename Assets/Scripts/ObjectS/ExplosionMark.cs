using System.Collections;
using Spawners;
using UnityEngine;

public class ExplosionMark : PooledObject
{
    [SerializeField] private float _destroyTime;
    [SerializeField] private float _fadeTime;

    private SpriteRenderer _renderer;
    private Color _initialColor;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _initialColor = _renderer.color;
    }

    private void OnEnable()
    {
        _renderer.color = _initialColor;
        StartCoroutine(HideMarkAfterDelay());
    }

    private IEnumerator HideMarkAfterDelay()
    {
        yield return new WaitForSeconds(_destroyTime);
        yield return FadeOut();
        Release();
    }

    private IEnumerator FadeOut()
    {
        var startColor = _renderer.color;
        float elapsed = 0f;

        while (elapsed < _fadeTime)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startColor.a, 0f, elapsed / _fadeTime);
            _renderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }
        _renderer.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
    }
}
