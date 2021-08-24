using UnityEngine;

public class Coin : MonoBehaviour
{
    public int CoinValue = 1;
    
    [SerializeField] private float maxTorque;

    private Rigidbody2D coinRigidbody;

    private void Start()
    {
        coinRigidbody = GetComponent<Rigidbody2D>();
        coinRigidbody.AddTorque(RandomTorque(), ForceMode2D.Force);
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
}
