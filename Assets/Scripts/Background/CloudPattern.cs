using System.Collections.Generic;
using UnityEngine;

public class CloudPattern : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _leftBound;
    [SerializeField] private Transform _respawnPosition;
    [Space]
    [SerializeField] private List<SpriteRenderer> _clouds;

    public void Initialize(Location location)
    {
        SetClouds(location);
    }

    private void SetClouds(Location location)
    {
        for (int i = 0; i < _clouds.Count; i++)
        {
            _clouds[i].sprite = location.cloudList[i];
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);

        if (transform.position.x < _leftBound)
        {
            transform.position = _respawnPosition.position;
        }
    }
}