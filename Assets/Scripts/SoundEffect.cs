using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundEffect : MonoBehaviour
{
    [SerializeField] private AudioClip alarm;

    // Reproducir un sonido al colisionar con el jugador
    private void OnTriggerEnter2D(Collider2D other)
    {
            SoundController.Instance.PlaySound(alarm);
    }
}