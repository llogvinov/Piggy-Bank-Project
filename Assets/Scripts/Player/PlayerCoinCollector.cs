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
        CollectCoin(other, coin);
    }

    private void CollectCoin(Collider2D other, Coin coin)
    {
        if (!Powerup.isDoubleCoinsActive)
            gameManager.coinToAdd += coin.coinValue;
        else
            gameManager.coinToAdd += 2 * coin.coinValue;

        playerAudio.PlayOneShot(coinClip, 1);

        Destroy(other.gameObject);
    }
}
