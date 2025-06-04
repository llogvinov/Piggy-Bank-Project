using System;
using Main;
using Spawners;
using UnityEngine;

public class Enemy : PooledObject
{
    public event Action<Vector3, float> Exploded, ExplodedOnGround;

    [SerializeField] private int damage;
    [SerializeField] private float damageRadius;
    [SerializeField] private float scale;

    protected const float groundCameraShakeForce = 0.05f;
    private const float playerCameraShakeForce = 0.1f;

    protected void Explode(float cameraShakeForce)
    {
        CameraShake.Shake(0.2f, cameraShakeForce);

        Exploded?.Invoke(transform.position, scale);

        Release();
    }

    protected void ExplodeOnGround()
    {
        DamageInRadius();

        ExplodedOnGround?.Invoke(transform.position, scale);

        Explode(groundCameraShakeForce);
    }

    protected void ExplodeOnPlayer(PlayerHealth playerHealth)
    {
        playerHealth.TakeDamage(damage);
        Explode(playerCameraShakeForce);
    }

    private void DamageInRadius()
    {
        if (damageRadius == 0) return;

        Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(transform.position, damageRadius);
        foreach (var overlapCollider in overlappedColliders)
        {
            if (overlapCollider.attachedRigidbody)
            {
                if (overlapCollider.attachedRigidbody.TryGetComponent<PlayerHealth>(out var playerHealth))
                {
                    if (playerHealth.enabled)
                    {
                        playerHealth.TakeDamage(damage);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
