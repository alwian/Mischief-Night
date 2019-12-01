using System.Collections;
using UnityEngine;


/*
 * Author: Amanda Norman (B00850615)
 *
 * Description: Abstract class which allows classes to inherit methods for audio
*/

public abstract class AbstractAudio : MonoBehaviour
{
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime) //Fade out method. Mainly used for transitions for Menus and credits
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * 0.1f;
            yield return null;
        }
        audioSource.Stop();
    }

    protected abstract void Play(); //Method that will select clips to play. Abstract.

    protected void Stop(AudioSource source){ //Stop audio
        AudioSource audio = source.GetComponent<AudioSource>();
        audio.Stop();
    }

    protected abstract void Next(); //Method that will play the next track in sequence. Abstract.

}