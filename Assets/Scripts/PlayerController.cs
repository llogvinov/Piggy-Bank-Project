using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer hatImage;
    [SerializeField] private SpriteRenderer maskImage;

    [SerializeField] private GameObject smallCrack;
    [SerializeField] private GameObject bigCrack;
    [SerializeField] private GameObject crackedPlayer;

    [SerializeField] private AudioClip coinClip;

    [SerializeField] private float playerSpeed;

    private bool facingRight = false;
    private bool moveLeft;
    private bool moveRight;
    private float horizontalInput;

    private Vector2 move;

    private AudioSource playerAudio;
    private Rigidbody2D playerRigitbody;
    private GameManager gameManager;
    private Animator playerAnimator;
    private PlayerHealth playerHealth;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerHealth = GetComponent<PlayerHealth>();
        playerRigitbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        playerAudio.volume = PlayerPrefs.GetFloat("sounds");

        SetHatAndMask();
    }

    private void SetHatAndMask()
    {
        Hat hat = GameDataManager.GetSelectedHat();
        Mask mask = GameDataManager.GetSelectedMask();

        hatImage.sprite = hat.image;
        maskImage.sprite = mask.image;
    }

    private void FixedUpdate()
    {
        if (!gameManager.isGameOver)
        {
            GetPlayerInput();
            SetAnimation();
            MovePlayerAndroid();
            HealthAndCracks();
        }

    }

    //Move player with buttons 
    public void TouchDownLeft() { moveLeft = true; }
    public void TouchUpLeft() { moveLeft = false; }
    public void TouchDownRight() { moveRight = true; }
    public void TouchUpRight() { moveRight = false; }
    
    private void GetPlayerInput()
    {
        if (moveLeft)
        {
            horizontalInput = -1f;
        }
        else if (moveRight)
        {
            horizontalInput = 1f;
        }
        else
        {
            horizontalInput = 0f;
        }
    }

    private void SetAnimation()
    {
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }
    }
    
    private void MovePlayerAndroid()
    {
        if (!Powerup.isSuperSpeedActive)
        {
            playerRigitbody.velocity = Vector2.right * horizontalInput * playerSpeed;
        }
        else
        {
            playerRigitbody.velocity = Vector2.right * horizontalInput * playerSpeed * Powerup.speedPowerupMultiplier;
        }
    }

    //Flip player
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //Connect player's health and cracks
    private void HealthAndCracks()
    {
        if (playerHealth.health == 3)
        {
            if (smallCrack.activeSelf) { smallCrack.SetActive(false); }
        }
        if (playerHealth.health == 2)
        {
            if (!smallCrack.activeSelf) { smallCrack.SetActive(true); }
            if (bigCrack.activeSelf) { bigCrack.SetActive(false); }
        }
        else if (playerHealth.health == 1)
        {
            if (!bigCrack.activeSelf) { bigCrack.SetActive(true); }
        }
        else if (playerHealth.health == 0)
        {
            Instantiate(crackedPlayer, transform.position, transform.rotation);
            gameManager.GameOver();

            Destroy(gameObject);
        }
    }

    //Collecting coins
    private void OnTriggerEnter2D(Collider2D other)
    {
        Coin coin = other.gameObject.GetComponent<Coin>();
        if (coin)
        {
            CollectCoin(other, coin);
        }
    }

    private void CollectCoin(Collider2D other, Coin coin)
    {
        if (!Powerup.isDoubleCoinsActive)
        {
            gameManager.coinToAdd += coin.coinValue;
        }
        else
        {
            gameManager.coinToAdd += 2 * coin.coinValue;
        }

        playerAudio.PlayOneShot(coinClip, 1);

        Destroy(other.gameObject);
    }

}
