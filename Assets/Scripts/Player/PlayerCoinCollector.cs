using Core;
using Core.Services.PlayerData;
using UnityEngine;

public class PlayerCoinCollector : MonoBehaviour
{
    [SerializeField] private AudioClip coinClip;

    private AudioSource playerAudio;
    private GameManager gameManager;

    private IPlayerDataService _playerDataService;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerAudio = GetComponent<AudioSource>();

        _playerDataService = AllServices.Container.Single<IPlayerDataService>();
        playerAudio.volume = _playerDataService.GetSound() == true ? 1f : 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.TryGetComponent(out Coin coin);

        if (coin != null)
            CollectCoin(coin);
    }

    private void CollectCoin(Coin coin)
    {
        if (!PowerUp.IsDoubleCoinsActive)
            gameManager.CoinToAdd += coin.CoinValue;
        else
            gameManager.CoinToAdd += 2 * coin.CoinValue;

        playerAudio.PlayOneShot(coinClip, 1);

        Destroy(coin.gameObject);
    }
}
