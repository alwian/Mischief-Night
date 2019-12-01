using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwitchAudio : DimensionedObject
{
    private AudioSource audio;
    public AudioClip woosh;
    protected override void SetOverworld()
    {
        audio.Play();
    }

    protected override void SetUpsideDown()
    {
        audio.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = woosh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
