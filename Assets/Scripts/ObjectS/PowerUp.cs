using System;
using Main;
using Spawners;
using UnityEngine;

public class PowerUp : PooledObject
{
    public static event Action<PowerupEventArgs> PowerupCollected;
    public static Action PowerupEnded;

    [SerializeField] public Sprite[] powerUpIcons;

    public static bool IsDoubleCoinsActive;

    public static bool IsShieldActive;

    public static bool IsSuperSpeedActive;
    public static float SpeedPowerUpMultiplier = 1.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponentInParent<Player>();
        if (player == null)
            return;

        int powerUpIndex = GetRandomPowerUp();
        PowerupCollected?.Invoke(new PowerupEventArgs()
        {
            index = powerUpIndex,
            Sprite = powerUpIcons[powerUpIndex]
        });

        Release();
    }

    private int GetRandomPowerUp() =>
        UnityEngine.Random.Range(0, 4);
}

public class PowerupEventArgs
{
    public int index;
    public Sprite Sprite;
}