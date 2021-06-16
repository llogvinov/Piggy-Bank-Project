using System.Collections.Generic;
using UnityEngine;

public class CrackedPlayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer hatImage;
    [SerializeField] private SpriteRenderer maskImage;
    [SerializeField] private GameObject explosionPosition;
    [SerializeField] private float explosionForce = 1000;

    [SerializeField] private List<Rigidbody2D> parts = new List<Rigidbody2D>();

    private void Start()
    {
        SetHatAndMask();
        FillPartsList();
        ApplyForceToParts();
    }

    private void SetHatAndMask()
    {
        Hat hat = GameDataManager.GetSelectedHat();
        Mask mask = GameDataManager.GetSelectedMask();

        hatImage.sprite = hat.image;
        maskImage.sprite = mask.image;
    }

    private void FillPartsList()
    {
        for (int i = 0; i < 6; i++)
        {
            parts.Add(gameObject.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>());
        }
    }

    private void ApplyForceToParts()
    {
        for (int i = 0; i < 5; i++)
        {
            AddExplosionForceCustom(parts[i], explosionForce, explosionPosition.transform.position);
        }
    }

    //Explode player particles when dead
    private void AddExplosionForceCustom(Rigidbody2D rb, float explosionForce,
        Vector2 explosionPosition, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force)
    {
        var explosionDir = rb.position - explosionPosition;
        var explosionDistance = explosionDir.magnitude;

        // Normalize without computing magnitude again
        if (upwardsModifier == 0)
            explosionDir /= explosionDistance;
        else
        {
            // From Rigidbody.AddExplosionForce doc:
            // If you pass a non-zero value for the upwardsModifier parameter, the direction
            // will be modified by subtracting that value from the Y component of the centre point.
            explosionDir.y += upwardsModifier;
            explosionDir.Normalize();
        }

        rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir, mode);
    }

}
