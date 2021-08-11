using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    
    [SerializeField] private float playerSpeed;

    private bool facingRight = false;

    private Rigidbody2D playerRigitbody;
    private Animator playerAnimator;
    private PlayerInput playerInput;

    private void Awake()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();

        playerRigitbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        if (!gameManager.isGameOver)
        {
            MovePlayerAndroid();
            SetAnimation();
        }
    }

    private void MovePlayerAndroid()
    {
        if (!Powerup.isSuperSpeedActive)
        {
            playerRigitbody.velocity = Vector2.right * playerInput.HorizontalInput * playerSpeed;
        }
        else
        {
            playerRigitbody.velocity = Vector2.right * playerInput.HorizontalInput * playerSpeed * Powerup.speedPowerupMultiplier;
        }
    }

    private void SetAnimation()
    {
        playerAnimator.SetFloat("Speed", Mathf.Abs(playerInput.HorizontalInput));

        if (playerInput.HorizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (playerInput.HorizontalInput < 0 && facingRight)
        {
            Flip();
        }
    }

    //Flip player
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

}
