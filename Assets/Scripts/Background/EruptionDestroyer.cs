using UnityEngine;

namespace Main.Background
{
    public class EruptionDestroyer : MonoBehaviour
    {
        public void DisableEruption() =>
            gameObject.SetActive(false);
    }
}