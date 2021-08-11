using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioClip coinClip;
    
    private AudioSource playerAudio;

    private void Start()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
        
        playerAudio = GetComponent<AudioSource>();
    }

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
