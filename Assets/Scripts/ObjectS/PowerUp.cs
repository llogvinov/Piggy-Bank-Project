using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] public Sprite[] powerUpIcons;

    private NormalGameManager gameManager;

    // double coins
    public static bool IsDoubleCoinsActive;
    private const float doubleCoinsPowerUpDuration = 10f;

    // shield
    public static bool IsShieldActive;
    private const float shieldPowerUpDuration = 6f;

    // super speed
    public static bool IsSuperSpeedActive;
    private const float superSpeedPowerUpDuration = 6f;
    public static float SpeedPowerUpMultiplier = 1.5f;

    private void Start()
    {
        gameManager = FindObjectOfType<NormalGameManager>();
    }

    //Collecting PowerUp
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.TryGetComponent(out Player player);
        if (player == null)
            return;
        
        int powerUpIndex = RandomPowerUp();
        switch (powerUpIndex)
        {
            case 0:
                StartTimer(doubleCoinsPowerUpDuration);
                StartCoroutine(DoubleCoinsPowerup());
                break;
            case 1:
                StartTimer(shieldPowerUpDuration);
                StartCoroutine(ShieldPowerUp());
                break;
            case 2:
                StartTimer(superSpeedPowerUpDuration);
                StartCoroutine(SuperSpeedPowerUp());
                break;
            case 3:
                StartCoroutine(HeartPowerUp());
                break;
        }
    }

    private void StartTimer(float duration)
    {
        if (gameManager.Timer)
            gameManager.Timer.StartNewTimer(duration);
    }
    
    public static void DeactivateAllPowerUps()
    {
        if (IsDoubleCoinsActive) 
            IsDoubleCoinsActive = false;

        if (IsShieldActive) 
            IsShieldActive = false;

        if (IsSuperSpeedActive) 
            IsSuperSpeedActive = false;
    }

    private int RandomPowerUp()
    {
        return Random.Range(0, 4);
    }

    //PowerUp that doubles coin values
    private IEnumerator DoubleCoinsPowerup()
    {
        IsDoubleCoinsActive = true;
        ActivatePowerUp(0);

        yield return new WaitForSeconds(doubleCoinsPowerUpDuration);

        IsDoubleCoinsActive = false;
        DeactivatePowerUp();
    }

    //PowerUp that prevent the player from taking damage
    private IEnumerator ShieldPowerUp()
    {
        IsShieldActive = true;
        ActivatePowerUp(1);
        
        yield return new WaitForSeconds(shieldPowerUpDuration);
        
        IsShieldActive = false;
        DeactivatePowerUp();
    }

    //PowerUp that speeds up the player
    private IEnumerator SuperSpeedPowerUp()
    {
        IsSuperSpeedActive = true;
        ActivatePowerUp(2);
        
        yield return new WaitForSeconds(superSpeedPowerUpDuration);
        
        IsSuperSpeedActive = false;
        DeactivatePowerUp();
    }

    //PowerUp that add 1 heart to players health
    private IEnumerator HeartPowerUp()
    {
        FindObjectOfType<PlayerHealth>().AddHeart();
        ActivatePowerUp(3);
        
        yield return new WaitForSeconds(2);

        DeactivatePowerUp();
    }

    private void ActivatePowerUp(int powerUpIndex)
    {
        gameManager.PowerupIcon.sprite = powerUpIcons[powerUpIndex];
        gameManager.PowerupIcon.gameObject.SetActive(true);
        
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    private void DeactivatePowerUp()
    {
        gameManager.PowerupIcon.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
