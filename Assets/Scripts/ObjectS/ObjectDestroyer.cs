using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Coin>() || 
            other.gameObject.GetComponent<Cloud>())
        {
            Destroy(other.gameObject);
        }
    }
}
