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
    [SerializeField] AudioSource dimensionSwitchAudioSource;

    protected override void SetOverworld()
    {
        dimensionSwitchAudioSource.Play();
    }

    protected override void SetUpsideDown()
    {
        dimensionSwitchAudioSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        dimensionSwitchAudioSource.Play();
    }
}
