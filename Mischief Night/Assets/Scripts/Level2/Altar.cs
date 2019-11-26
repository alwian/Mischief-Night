using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Altar : MonoBehaviour, IInteractable
{
    [Header("Required References")] 
    [SerializeField] Level2Manager manager;

    [SerializeField] GameObject beamEffect;
    [SerializeField] AudioClip activationSound;

    new AudioSource audio;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        manager.TryActivateAltar(this);
    }

    public void Activate()
    {
        beamEffect.SetActive(true);
        audio.PlayOneShot(activationSound);
    }
}
