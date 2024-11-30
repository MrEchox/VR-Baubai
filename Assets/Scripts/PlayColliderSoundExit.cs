using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayColliderSoundExit : MonoBehaviour
{
    private AudioSource audioSource;
    private bool hasPlayed = false;

    private void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        if (audioSource == null)
            Debug.LogError("No AudioSource found in children of " + gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            Console.Write("Playing sound!");
            audioSource.Play();
            hasPlayed = true;
        }
    }
}
