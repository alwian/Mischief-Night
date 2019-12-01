using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelManager : MonoBehaviour
{
    [Header("Player Spawn")]
    [SerializeField] protected Transform spawnPoint;

    [Header("Level Title")]
    [SerializeField] Canvas levelTitle;
    [SerializeField] float titleDisplayTime = 5f;

    public abstract void StartLevel();

    protected void ShowTitle()
    {
        StartCoroutine(ShowLevelTitle());
    }

    IEnumerator ShowLevelTitle()
    {
        levelTitle.gameObject.SetActive(true);
        yield return new WaitForSeconds(titleDisplayTime);
        levelTitle.gameObject.SetActive(false);
    }
}
