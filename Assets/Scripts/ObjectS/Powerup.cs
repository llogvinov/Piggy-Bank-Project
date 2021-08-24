using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup : MonoBehaviour
{
    [SerializeField] public Sprite[] powerupIcons;

    private NormalGameManager gameManager;

    public static bool isDoubleCoinsActive;
    private float doubleCoinsPowerupDuration = 10f;

    public static bool isShildActive;
    private float shildPowerupDuration = 6f;

    public static bool isSuperSpeedActive;
    private float superSpeedPowerupDuration = 6f;
    public static float speedPowerupMultiplier = 1.5f;

    private void Awake()
    {
        gameManager = FindObjectOfType<NormalGameManager>();
    }

    //Collecting powerup
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.TryGetComponent(out Player player);
        
        if (player == null)
            return;
        
        int powerupIndex = RandomPowerup();
        Debug.Log("powerup index: " + powerupIndex);

        switch (powerupIndex)
        {
            case 0:
                if (gameManager.Timer) { gameManager.Timer.StartNewTimer(doubleCoinsPowerupDuration); }
                else { Debug.Log("Не нашлось :((("); }
                StartCoroutine(DoubleCoinsPowerup());
                break;
            case 1:
                if (gameManager.Timer) { gameManager.Timer.StartNewTimer(shildPowerupDuration); }
                else { Debug.Log("Не нашлось :((("); }
                StartCoroutine(ShildPowerup());
                break;
            case 2:
                if (gameManager.Timer) { gameManager.Timer.StartNewTimer(superSpeedPowerupDuration); }
                else { Debug.Log("Не нашлось :((("); }
                StartCoroutine(SuperSpeedPowerup());
                break;
            case 3:
                StartCoroutine(HeartPowerup());
                break;
        }
    }

    public static void DeactivatePowerups()
    {
        if (isDoubleCoinsActive) 
            isDoubleCoinsActive = false;

        if (isShildActive) 
            isShildActive = false;

        if (isSuperSpeedActive) 
            isSuperSpeedActive = false;
    }

    public static int RandomPowerup()
    {
        return Random.Range(0, 4);
    }

    //Powerup that doubles coin values
    private IEnumerator DoubleCoinsPowerup()
    {
        gameManager.PowerupIcon.sprite = powerupIcons[0];
        gameManager.PowerupIcon.gameObject.SetActive(true);
        isDoubleCoinsActive = true;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(doubleCoinsPowerupDuration);

        isDoubleCoinsActive = false;
        gameManager.PowerupIcon.gameObject.SetActive(false);

        Destroy(gameObject);
    }

    //Powerup that prevent the player from taking damage
    private IEnumerator ShildPowerup()
    {
        gameManager.PowerupIcon.sprite = powerupIcons[1];
        gameManager.PowerupIcon.gameObject.SetActive(true);
        isShildActive = true;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(shildPowerupDuration);
        
        isShildActive = false;
        gameManager.PowerupIcon.gameObject.SetActive(false);

        Destroy(gameObject);
    }

    //Powerup that speeds up the player
    private IEnumerator SuperSpeedPowerup()
    {
        gameManager.PowerupIcon.sprite = powerupIcons[2];
        gameManager.PowerupIcon.gameObject.SetActive(true);
        isSuperSpeedActive = true;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(superSpeedPowerupDuration);
        
        isSuperSpeedActive = false;
        gameManager.PowerupIcon.gameObject.SetActive(false);

        Destroy(gameObject);
    }

    //Powerup that add 1 heart to players health
    private IEnumerator HeartPowerup()
    {
        gameManager.PowerupIcon.sprite = powerupIcons[3];
        gameManager.PowerupIcon.gameObject.SetActive(true);

        PlayerHealth player = FindObjectOfType<PlayerHealth>();
        player.AddHeart();

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(2);
        
        gameManager.PowerupIcon.gameObject.SetActive(false);

        Destroy(gameObject);
    }

}
