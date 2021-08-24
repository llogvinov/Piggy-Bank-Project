using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;

    private bool facingRight = false;

    private Rigidbody2D playerRigitbody;
    private Animator playerAnimator;
    private PlayerInput playerInput;
    private GameManager gameManager;
    
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerRigitbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        if (gameManager.IsGameOver)
            return;
        
        MovePlayer();
        SetAnimation();
    }

    private void SetAnimation()
    {
        playerAnimator.SetFloat(Speed, Mathf.Abs(playerInput.HorizontalInput));

        if (playerInput.HorizontalInput > 0 && !facingRight)
            Flip();
        else if (playerInput.HorizontalInput < 0 && facingRight)
            Flip();
    }
    
    private void MovePlayer()
    {
        if (!PowerUp.IsSuperSpeedActive)
            playerRigitbody.velocity = Vector2.right * playerInput.HorizontalInput * playerSpeed;
        else
            playerRigitbody.velocity = Vector2.right * playerInput.HorizontalInput * playerSpeed * PowerUp.SpeedPowerUpMultiplier;
    }

    //Flip player
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
