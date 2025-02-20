using System.Collections;
using UnityEngine;

namespace Main.Background
{
    public class Eruption : MonoBehaviour
    {
        [SerializeField] private GameObject eruptionPrefab;
        [SerializeField] private Transform eruptionPosition;

        [SerializeField] private float minTimeEruption = 5;
        [SerializeField] private float maxTimeEruption = 10;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minTimeEruption, maxTimeEruption));
                Instantiate(eruptionPrefab, eruptionPosition.position, Quaternion.identity, transform);
            }
        }
    }
}