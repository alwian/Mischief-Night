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

    public virtual void StartLevel()
    {
        var player = GameManager.Instance.Player;
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;

        ShowTitle();

        DimensionManager.Cleanup();
        CameraManager.Instance.Reset();
    }

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
