using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
