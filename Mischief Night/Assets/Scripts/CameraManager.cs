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
    [SerializeField] Camera cameraPrefab;
    [SerializeField] Image blackFader;
    [SerializeField] Image deathFader;

    [Header("Options")]
    [SerializeField] [Range(0f, 1f)] float deathFaderOpacity = 0.95f;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            camera = Instantiate(cameraPrefab);

            this.transform.parent = null;
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(camera.gameObject);
        }
        else
        {
            Debug.Log("Destroying duplicate CameraManager...");
            Destroy(this);
        }
    }

    public void Reset()
    {
        Color col = blackFader.color;
        col.a = 0f;
        blackFader.color = col;

        col = deathFader.color;
        col.a = 0f;
        deathFader.color = col;
    }

    public void DeathFadeOut(float time)
    {
        StopAllCoroutines();
        StartCoroutine(Fade(time, deathFader, deathFaderOpacity));
    }

    public void FadeOut(float time)
    {
        StopAllCoroutines();
        StartCoroutine(Fade(time, blackFader, 1f));
    }

    public void FadeIn(float time)
    {
        StopAllCoroutines();
        StartCoroutine(Fade(time, blackFader, 0f));
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
