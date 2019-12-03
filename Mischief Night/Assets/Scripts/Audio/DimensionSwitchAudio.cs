using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Amanda Norman (B00850615)
*
* Description: Audio manager class to manage the audio for changing dimensions in game
*/

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

}
