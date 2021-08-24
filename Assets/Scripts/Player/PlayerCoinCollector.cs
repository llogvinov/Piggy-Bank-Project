using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoinCollector : MonoBehaviour
{
    [SerializeField] private AudioClip coinClip;

    private AudioSource playerAudio;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerAudio = GetComponent<AudioSource>();

        playerAudio.volume = PlayerPrefs.GetFloat("sounds");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.TryGetComponent(out Coin coin);
        
        if (coin != null)
            CollectCoin(coin);
    }

    private void CollectCoin(Coin coin)
    {
        if (!Powerup.isDoubleCoinsActive)
            gameManager.CoinToAdd += coin.coinValue;
        else
            gameManager.CoinToAdd += 2 * coin.coinValue;

        playerAudio.PlayOneShot(coinClip, 1);

        Destroy(coin.gameObject);
    }
}
