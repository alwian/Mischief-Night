/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class SoundTrigger : MonoBehaviour
{
    [SerializeField] bool playsOnce;
    bool hasPlayed;

    AudioSource audioS;
    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( (!hasPlayed || !playsOnce) && other.CompareTag("Player"))
        {
            hasPlayed = true;
            audioS.Play();
        }
    }
}
