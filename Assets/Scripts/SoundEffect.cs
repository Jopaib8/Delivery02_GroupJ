using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundEffect : MonoBehaviour
{
    [SerializeField] private AudioClip Alarm;

    private void OnTriggerEnter2D(Collider2D other)
    {
            SoundController.Instance.PlaySound(Alarm);
    }
}