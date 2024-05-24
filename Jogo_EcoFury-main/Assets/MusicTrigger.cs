using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioClip backgroundMusic;
    private bool musicStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Play er") && !musicStarted)
        {
            AudioSource audioSource = Camera.main.GetComponent<AudioSource>(); // Obt�m o AudioSource da c�mera principal
            audioSource.clip = backgroundMusic;
            audioSource.Play();
            musicStarted = true;
        }
    }
}

