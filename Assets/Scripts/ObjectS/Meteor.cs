using UnityEngine;

public class Meteor : Enemy
{
    private SurvivalGameManager gameManager;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();

        gameManager = FindObjectOfType<SurvivalGameManager>();
    }

    private void FixedUpdate()
    {
        if (gameManager.IsGameOver || gameManager.currentTime <= 0)
        {
            Explode(groundCameraShakeForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
