using System.Collections;
using UnityEngine;

public class ExplosionMark : MonoBehaviour
{
    [SerializeField] private float destroyTime;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

}
