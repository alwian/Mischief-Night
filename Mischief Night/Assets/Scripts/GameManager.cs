/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player Player { get; private set; }

    [SerializeField] Player playerPrefab;
    [SerializeField] Transform spawnPoint;

    [SerializeField] float levelTitleDisplayTime = 5f;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
        {
            Debug.Log("Destroying duplicate GameManager...");
            Destroy(this);
        }

        Player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        var lvlManager = GameObject.FindObjectOfType<LevelManager>();
        if (lvlManager)
            lvlManager.ShowTitle(levelTitleDisplayTime);
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
