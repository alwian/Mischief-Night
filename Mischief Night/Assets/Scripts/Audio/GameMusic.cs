using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Amanda Norman (B00850615)
 * 
 * Description: Class which controls the Ambient Game Music throughout the scene.
*/

public class GameMusic : AbstractAudio
{
    private new AudioSource audio;
    public AudioClip[] audioClip;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        Play();
    }

    protected override void Play()
    {
        audio.clip = audioClip[0]; //finds clip from array of clips
        audio.Play(); //plays track
        Invoke("Next", audio.clip.length); //calls the play next track method when track finishes playing
    }

    protected override void Next(){
        audio.Stop(); //stops other audio just in case
        audio.clip = audioClip[0]; //finds next track in the array
        audio.Play(); //plays track
        Invoke("Play", audio.clip.length); //calls the play music track again when track finishes playing
    }
}
