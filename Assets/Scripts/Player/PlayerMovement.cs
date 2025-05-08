using UnityEngine;
using YG;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;

    private bool facingRight = false;

    private Rigidbody2D playerRigitbody;
    private Animator playerAnimator;
    private PlayerInput playerInput;

    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Start()
    {
        playerRigitbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

#if UNITY_EDITOR
        playerInput = GetComponent<PlayerComputerInput>();
#elif UNITY_WEBGL
        if (YG2.envir.isMobile || YG2.envir.isMobile)
        {
            playerInput = GetComponent<PlayerTouchInput>();
        }
        else if (YG2.envir.isDesktop)
        {
            playerInput = GetComponent<PlayerComputerInput>();
        }
#endif
    }

    private void FixedUpdate()
    {
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

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
