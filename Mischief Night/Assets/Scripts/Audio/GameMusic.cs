using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : AbstractAudio
{
    private AudioSource audio;
    public AudioClip[] audioClip;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void Play()
    {
        AudioSource source = audio.GetComponent<AudioSource>(); //creates new audio source
        audio.clip = audioClip[0]; //finds clip from array of clips
        audio.Play(); //plays track
        Invoke("Next", audio.clip.length); //calls the play next track method when track finishes playing
    }

    protected override void Next(){
        AudioSource source = audio.GetComponent<AudioSource>(); //creates a new audio source
        audio.Stop(); //stops other audio just in case
        audio.clip = audioClip[0]; //finds next track in the array
        audio.Play(); //plays track
        Invoke("Play", audio.clip.length); //calls the play music track again when track finishes playing
    }
}
