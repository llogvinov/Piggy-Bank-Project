using Main.Background;
using Spawners;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PooledObject>(out var pooledObject))
        {
            pooledObject.Release();
        }
        else if (other.gameObject.GetComponent<Cloud>())
        {
            Destroy(other.gameObject);
        }
    }
}
