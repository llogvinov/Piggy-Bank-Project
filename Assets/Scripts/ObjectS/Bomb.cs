using UnityEngine;

public class Bomb : Enemy
{
    [SerializeField] private float maxTorque;

    private Rigidbody2D bombRigidbody;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        bombRigidbody = GetComponent<Rigidbody2D>();

        bombRigidbody.AddTorque(RandomTorque(), ForceMode2D.Force);
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Powerup.isShildActive)
        {
            Explode(groundCameraShakeForce);
            return;
        }

        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player)
        {
            ExplodeOnPlayer();
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            ExplodeOnGround();
        }
        else
        {
            Explode(groundCameraShakeForce);
        }
    }
}
