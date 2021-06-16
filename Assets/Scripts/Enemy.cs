using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject explosionMarkPrefab;
    [Space]
    [SerializeField] protected int damage;
    [SerializeField] protected float damageRadius;
    [SerializeField] private float scale;

    protected PlayerHealth playerHealth;

    //Calls when bomb hits cracked player
    protected void JustExplode()
    {
        CameraShake.Shake(0.2f, 0.05f);

        GameObject explosion = Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        explosion.transform.localScale *= scale;

        Destroy(gameObject);
    }

    //Calls when the bomb hits the ground
    protected void ExplodeOnGround()
    {
        Explosion();

        GameObject mark = Instantiate(explosionMarkPrefab, transform.position, explosionMarkPrefab.transform.rotation);
        mark.transform.localScale *= scale;
    }

    //Calls when the bomb hits the player
    protected void ExplodeOnPlayer()
    {
        playerHealth.TakeDamage(damage);

        CameraShake.Shake(0.2f, 0.1f);

        GameObject explosion = Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        explosion.transform.localScale *= scale;

        Destroy(gameObject);
    }

    //If player in damage radius damage first
    protected void Explosion()
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

        CameraShake.Shake(0.2f, 0.05f);

        GameObject explosion = Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        explosion.transform.localScale *= scale;

        Destroy(gameObject);
    }

    //Show damage radius in scene window
    protected void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }

}
