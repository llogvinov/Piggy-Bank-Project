using System.Collections.Generic;
using Core;
using Core.Services.PlayerData;
using UnityEngine;

namespace Main
{
    public class CrackedPlayer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _hatImage;
        [SerializeField] private SpriteRenderer _maskImage;
        [Space]
        [SerializeField] private GameObject _explosionPosition;
        [SerializeField] private float _explosionForce = 1000;
        [Space]
        [SerializeField] private List<Rigidbody2D> _parts = new List<Rigidbody2D>();

        private IPlayerDataService _playerDataService;
        
        private void Awake()
        {
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
        }

        private void Start()
        {
            SetHatAndMask();
            FillPartsList();
            ApplyForceToParts();
        }

        private void SetHatAndMask()
        {
            Hat hat = _playerDataService.GetSelectedHat();
            Mask mask = _playerDataService.GetSelectedMask();

            _hatImage.sprite = hat.Image;
            _maskImage.sprite = mask.Image;
        }

        private void FillPartsList()
        {
            for (int i = 0; i < 6; i++)
            {
                _parts.Add(gameObject.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>());
            }
        }

        private void ApplyForceToParts()
        {
            for (int i = 0; i < 5; i++)
            {
                AddExplosionForceCustom(_parts[i], _explosionForce, _explosionPosition.transform.position);
            }
        }

        private void AddExplosionForceCustom(Rigidbody2D rb,
            float explosionForce,
            Vector2 explosionPosition,
            float upwardsModifier = 0f,
            ForceMode2D mode = ForceMode2D.Force)
        {
            var explosionDir = rb.position - explosionPosition;
            var explosionDistance = explosionDir.magnitude;

            if (upwardsModifier == 0)
            {
                explosionDir /= explosionDistance;
            }
            else
            {
                explosionDir.y += upwardsModifier;
                explosionDir.Normalize();
            }

            rb.AddForce(Mathf.Lerp(0, explosionForce, 1 - explosionDistance) * explosionDir, mode);
        }

    }
}