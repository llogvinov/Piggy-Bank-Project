using Main;
using UnityEngine;

public class Bomb : Enemy
{
    [SerializeField] private float maxTorque;

    private void Start()
    {
        Rigidbody.AddTorque(RandomTorque(), ForceMode2D.Force);
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out var playerHealth))
        {
            if (PowerUp.IsShieldActive)
            {
                Explode(groundCameraShakeForce);
                return;
            }
            else
            {
                ExplodeOnPlayer(playerHealth);
            }
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
