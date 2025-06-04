using Core;
using Main;
using UnityEngine;

public class Meteor : Enemy
{
    private void Start()
    {
        Game.GameOver += OnGameOver;
    }

    private void OnDestroy()
    {
        Game.GameOver -= OnGameOver;
    }

    private void OnGameOver(GameOverCondition condition)
    {
        if (gameObject.activeSelf)
        {
            Explode(groundCameraShakeForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out var playerHealth))
        {
            if (playerHealth.enabled)
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
