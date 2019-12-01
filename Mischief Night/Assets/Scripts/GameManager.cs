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

    private void Awake()
    {
        if (Instance)
        {
            Debug.Log("Destroying duplicate GameManager...");
            Destroy(this);
            return;
        }

        Instance = this;
        Player = Instantiate(playerPrefab);

        this.transform.parent = null;
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(Player.gameObject);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void StartLevel(AsyncOperation op)
    {
        StartLevel();
    }
    public void StartLevel()
    {
        var lvlManager = GameObject.FindObjectOfType<LevelManager>();
        if (lvlManager)
            lvlManager.StartLevel();
    }

    public void Reload()
    {
        Instance = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy(this.gameObject);
    }

}
