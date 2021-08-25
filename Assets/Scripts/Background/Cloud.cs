using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float speed;

    private void Start() => speed = transform.parent.GetComponent<CloudSpawner>().Speed;

    private void Update() => transform.Translate(Vector3.left * speed * Time.deltaTime);
}
