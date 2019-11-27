using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelManager : MonoBehaviour
{
    [SerializeField] Canvas levelTitle;

    public void ShowTitle(float time)
    {
        StartCoroutine(ShowLevelTitle(time));
    }

    IEnumerator ShowLevelTitle(float time)
    {
        levelTitle.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        levelTitle.gameObject.SetActive(false);
    }
}
