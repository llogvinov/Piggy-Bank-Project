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
        if (PowerUp.IsShieldActive)
        {
            Explode(groundCameraShakeForce);
            return;
        }

        collision.gameObject.TryGetComponent(out PlayerHealth player);
        if (player)
            ExplodeOnPlayer();
        else if (collision.gameObject.CompareTag("Ground"))
            ExplodeOnGround();
        else
            Explode(groundCameraShakeForce);
    }
}
