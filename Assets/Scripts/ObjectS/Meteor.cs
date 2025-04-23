using Core;
using Main;
using UnityEngine;

public class Meteor : Enemy
{
    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

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
        Explode(groundCameraShakeForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player)
            ExplodeOnPlayer();
        else if (collision.gameObject.CompareTag("Ground"))
            ExplodeOnGround();
        else
            Explode(groundCameraShakeForce);
    }

}
