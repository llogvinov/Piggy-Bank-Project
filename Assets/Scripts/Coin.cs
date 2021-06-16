using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] public int coinValue = 1;

    [SerializeField] private float maxTorque;

    private Rigidbody2D coinRigidbody;

    private void Awake()
    {
        coinRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        coinRigidbody.AddTorque(RandomTorque(), ForceMode2D.Force);
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
}