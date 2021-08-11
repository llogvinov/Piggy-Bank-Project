using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float speed;

    private void Start()
    {
        speed = transform.parent.GetComponent<CloudSpawner>().speed;
    }

    private void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }
}
