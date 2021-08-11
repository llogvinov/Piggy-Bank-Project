using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource gameAudio;

    private static MusicManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        gameAudio.volume = PlayerPrefs.GetFloat("music");
    }

}
