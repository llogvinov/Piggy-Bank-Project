using UnityEngine;

namespace Main.Background
{
    public class EruptionDestroyer : MonoBehaviour
    {
        public void DestroyEruption() =>
            Destroy(gameObject);
    }
}