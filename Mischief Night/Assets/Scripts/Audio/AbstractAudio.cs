using System.Collections;
using UnityEngine;

public abstract class AbstractAudio : MonoBehaviour
{
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

    protected abstract void Play();

    protected void Stop(AudioSource source){
        AudioSource audio = source.GetComponent<AudioSource>();
        audio.Stop();
    }

    protected abstract void Next();

}