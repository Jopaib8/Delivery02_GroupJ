using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;
    private AudioSource audioSource;

    // Si no hay una instancia de SoundController, se crea una y se mantiene en todas las escenas

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    // Reproducir un sonido
    public void PlaySound (AudioClip sonido)
    {
        audioSource.PlayOneShot(sonido);
    }
}
