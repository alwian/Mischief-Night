using UnityEngine;

public abstract class AbstractAudio : MonoBehaviour
{
    private AudioClip clip;

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * 0.1f;

            yield return null;
        }

        audioSource.Stop();
    }

    protected abstract void Play(){
        AudioSource audio = GetComponent<AudioSource>(); //creates new audio source
        audio.clip = audioClip[0]; //finds clip from array of clips
        audio.Play(); //plays track
        Invoke("playNextTrack", audio.clip.length); //calls the play next track method when track finishes playing
    }

    protected abstract void Stop(){
        AudioSource audio = GetComponent<AudioSource>();
        audio.stop();
    }

    protected abstract void Next(){
        AudioSource audio = GetComponent<AudioSource>(); //creates a new audio source
        audio.Stop(); //stops other audio just in case
        audio.clip = audioClip[1]; //finds next track in the array
        audio.Play(); //plays track
        Invoke("playMusic", audio.clip.length); //calls the play music track again when track finishes playing
    }

}