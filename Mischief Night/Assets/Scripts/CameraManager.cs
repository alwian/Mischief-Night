/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    new Camera camera;

    [Header("Required References")]
    [SerializeField] Image blackFader;
    [SerializeField] Image deathFader;

    [Header("Options")]
    [SerializeField] [Range(0f, 1f)] float deathFaderOpacity = 0.95f;


    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
        {
            Debug.Log("Destroying duplicate CameraManager...");
            Destroy(this);
        }

        camera = Camera.main;
    }

    public void DeathFadeOut(float time)
    {
        StopAllCoroutines();
        StartCoroutine(Fade(time, deathFader, deathFaderOpacity));
    }

    public void FadeOut(float time)
    {
        StopAllCoroutines();
        StartCoroutine(Fade(time, deathFader, 1f));
    }

    public void FadeIn(float time)
    {
        StopAllCoroutines();
        StartCoroutine(Fade(time, deathFader, 0f));
    }

    private IEnumerator Fade(float time, Image fader, float targetOpacity)
    {
        Color col;

        Color startCol = fader.color;
        Color targetCol = startCol;
        targetCol.a = targetOpacity;

        float progress = 0f;
        while(progress <= time)
        {
            progress += Time.deltaTime;

            col = Color.Lerp(startCol, targetCol, progress/time);
            fader.color = col;

            yield return null;
        }
    }
}
