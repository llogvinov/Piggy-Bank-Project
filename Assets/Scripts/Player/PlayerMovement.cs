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

    private void Awake()
    {
        playerRigitbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable() =>
        Reset();

    private void OnDisable() =>
        Reset();

    private void Start()
    {
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

    private void FixedUpdate() =>
        MovePlayer();

    private void Update() =>
        SetAnimation();

    private void Reset()
    {
        playerRigitbody.velocity = Vector2.zero;
        playerAnimator.SetFloat(Speed, 0f);
    }

    private void MovePlayer()
    {
        if (!PowerUp.IsSuperSpeedActive)
            playerRigitbody.velocity = Vector2.right * playerInput.HorizontalInput * playerSpeed;
        else
            playerRigitbody.velocity = Vector2.right * playerInput.HorizontalInput * playerSpeed * PowerUp.SpeedPowerUpMultiplier;
    }

    private void SetAnimation()
    {
        playerAnimator.SetFloat(Speed, Mathf.Abs(playerRigitbody.velocity.x));

        if (playerInput.HorizontalInput > 0 && !facingRight)
            Flip();
        else if (playerInput.HorizontalInput < 0 && facingRight)
            Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
