using System.Collections;
using UnityEngine;

public class Eruption : MonoBehaviour
{
    [SerializeField] private GameObject eruptionPrefab;
    [SerializeField] private GameObject eruptionPosition;

    [SerializeField] private float minTimeEruption = 5;
    [SerializeField] private float maxTimeEruption = 10;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeEruption, maxTimeEruption));

            Instantiate(eruptionPrefab, eruptionPosition.transform.position, eruptionPrefab.transform.rotation);
        }
    }

}
