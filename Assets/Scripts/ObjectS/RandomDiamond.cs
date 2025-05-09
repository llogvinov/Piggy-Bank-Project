using System.Collections.Generic;
using UnityEngine;

public class RandomDiamond : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [Space]
    [SerializeField] private List<Sprite> _spriteList;

    private void OnEnable()
    {
        _renderer.sprite = _spriteList[Random.Range(0, _spriteList.Count)];
    }
}
