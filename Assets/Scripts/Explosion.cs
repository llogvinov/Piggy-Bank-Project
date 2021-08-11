using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource explosionAudio;

    private void Awake()
    {
        explosionAudio = GetComponent<AudioSource>();

        explosionAudio.volume = PlayerPrefs.GetFloat("sounds");
    }

    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}
