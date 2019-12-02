using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneAudio : AbstractAudio
{
    public AudioClip[] sbClips;

    private new AudioSource audio;
    protected override void Next()
    {
        throw new System.NotImplementedException();
    }

    protected override void Play()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SoulBringerAudio()
    {
        GameObject sb = GameObject.FindGameObjectWithTag("SoulBringer");
        AudioSource audioSource = sb.GetComponent<AudioSource>();
        Patrol sPat = new Patrol();

        switch (sPat.GetState()){
            case "Walk":
                audioSource.clip = sbClips[0];
                audioSource.Play();
                break;
            case "Run":
                audioSource.clip = sbClips[1];
                audioSource.Play();
                break;
            case "Attack":
                audioSource.clip = sbClips[2];
                audioSource.Play();
                break;
        }
    }

}
