using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] Image fader;
    new Camera camera;

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

    public void FadeOut(float time)
    {
        StopAllCoroutines();
        StartCoroutine(Fade(time, 1f));
    }

    public void FadeIn(float time)
    {
        StopAllCoroutines();
        StartCoroutine(Fade(time, 0f));
    }

    private IEnumerator Fade(float time, float targetOpacity)
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
