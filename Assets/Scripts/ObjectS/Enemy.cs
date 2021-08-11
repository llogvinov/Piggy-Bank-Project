using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject explosionMarkPrefab;
    [Space]
    [SerializeField] private int damage;
    [SerializeField] private float damageRadius;
    [SerializeField] private float scale;

    protected PlayerHealth playerHealth;
    
    protected const float groundCameraShakeForce = 0.05f;
    protected const float playerCameraShakeForce = 0.1f;

    //Calls when the shild is active
    //or bomb hits cracked player
    protected void Explode(float cameraShakeForce)
    {
        CameraShake.Shake(0.2f, cameraShakeForce);

        GameObject explosion = Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        explosion.transform.localScale *= scale;

        Destroy(gameObject);
    }

    //Calls when the bomb hits the ground
    protected void ExplodeOnGround()
    {
        DamageInRadius();

        Explode(groundCameraShakeForce);

        GameObject mark = Instantiate(explosionMarkPrefab, transform.position, explosionMarkPrefab.transform.rotation);
        mark.transform.localScale *= scale;
    }

    //Calls when the bomb hits the player
    protected void ExplodeOnPlayer()
    {
        playerHealth.TakeDamage(damage);

        Explode(playerCameraShakeForce);
    }

    //If player in damage radius damage first
    private void DamageInRadius()
    {
        Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(transform.position, damageRadius);

        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            Rigidbody2D rigidbody = overlappedColliders[i].attachedRigidbody;
            if (rigidbody)
            {
                if (rigidbody.GetComponent<PlayerController>())
                {
                    playerHealth.TakeDamage(damage);
                }
            }
        }
    }

    //Show damage radius in scene window
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }

}
